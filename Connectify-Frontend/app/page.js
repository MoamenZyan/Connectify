"use client";
import * as signalR from "@microsoft/signalr";
import styles from "./page.module.css";

import LeftNavBar from "@/components/leftNavBar/leftNavBar.module";
import RightBox from "@/components/rightBox/rightBox.module";
import GetCurrentUserInfo from "@/ApiHelpers/UserAPIs/getCurrentUserInfo";
import GetAllUsers from "@/ApiHelpers/UserAPIs/getAllUsers";
import { useRouter } from "next/navigation";
import { useEffect, useState, useRef } from "react";
import SendFriendRequest from "@/ApiHelpers/UserAPIs/SendFriendRequest";
import DeleteFriendRequest from "@/ApiHelpers/UserAPIs/DeleteFriendRequest";
import AcceptFriendRequest from "@/ApiHelpers/UserAPIs/AcceptFriendRequest";
import UploadMessageAttachment from "@/ApiHelpers/MessageAPIs/UploadMessageAttachment";
import { useCallback } from "react";

export default function Home() {
    const [currentUser, setCurrentUser] = useState(null);
    const [connection, setConnection] = useState(null);
    const [currentChat, setCurrentChat] = useState(null);
    const [receiverUser, setReceiverUser] = useState(null);
    const [chatTrigger, setChatTrigger] = useState(false);
    const [sentFriendRequests, setSentFriendRequests] = useState(new Map());
    const [users, setUsers] = useState(new Map());
    const [loading, setLoading] = useState(true);
    const [privateChats, setPrivateChats] = useState(new Map());
    const [numberOfUnSeenMessages, setNumberOfUnSeenMessages] = useState(0);
    const [unSeenMessagesTrigger, setUnSeenMessagesTrigger] = useState(false);
    const [notifications, setNotifications] = useState(new Map());
    const [newNotification, setNewNotification] = useState(false);


    const chatScroll = useRef(null);
    const currentChatRef = useRef(currentChat);
    const currentUserRef = useRef(currentUser);
    const chatsRef = useRef(privateChats);
    const userIdToPrivateChatIdMapRef = useRef(new Map());
    const receiverRef = useRef(receiverUser);
    const sentFriendRequestsRef = useRef(sentFriendRequests);
    const router = useRouter();

    const getChatInfo = useCallback(async(entity, type) => {
        if (type == "group")
        {
            setCurrentChat(entity);
            console.log(entity);
            return;
        }
        if (type == "user")
        {
            setReceiverUser(entity);
            receiverRef.current = entity;
        }
        const result = chatsRef.current.get(userIdToPrivateChatIdMapRef.current.get(type == "user" ? entity.id : entity));
        if (result != undefined)
            setCurrentChat(result);
        else
            setCurrentChat(null);
    }, []);


    const getUserInfo = async () => {
        const result = await GetCurrentUserInfo(localStorage.getItem('token'));
        if (result.status == true)
        {
            for (let i = 0; i < result.user.groupChats.length; i++) {
                result.user.groupChats[i].messages = new Map(Object.entries(result.user.groupChats[i].messages));
            }
            setCurrentUser(result.user);
            setSentFriendRequests(new Map(Object.entries(result.user.sentFriendRequests)));
        }
        return {status: result.status, user: result.user};
    };

    useEffect(() => {
        async function getInfo() {
            await getUserInfo();
        }
        getInfo();
    }, [chatTrigger, notifications]);

    useEffect(() => {
        setNumberOfUnSeenMessages(0);
        scrollTop();
    }, [unSeenMessagesTrigger]);

    useEffect(() => {
        currentUserRef.current = currentUser;
    }, [currentUser])

    const getGlobalInfo = async () => {
        var result = await getUserInfo();
        console.log(result.user);
        if (result.status)
        {
            const users = await GetAllUsers();
            const usersMap = new Map();
            const chatsMap = new Map();
            for (let i = 0; i < users.length; i++) {
                usersMap.set(users[i].id, users[i]);
            }
            for (let i = 0; i < result.user.privateChats.length; i++) {
                var updatedChat = result.user.privateChats[i];
                userIdToPrivateChatIdMapRef.current.set(result.user.privateChats[i].users[0].id, result.user.privateChats[i].id);
                updatedChat.messages = new Map(Object.entries(result.user.privateChats[i].messages));
                chatsMap.set(result.user.privateChats[i].id, updatedChat);
            }
            const notifications = [...result.user.infoNotifications, ...result.user.associatedInfoNotifications];
            const notificationsMap = new Map();
            for (let i = 0; i < notifications.length; i++) {
                notificationsMap.set(notifications[i].id, notifications[i]);
            }

            setNotifications(notificationsMap);
            chatsRef.current = chatsMap;
            setPrivateChats(chatsMap);
            setUsers(usersMap);
            setLoading(false);
            return result.user;
        }
        else
        {
            localStorage.removeItem('token');
            router.push("/login");   
        }
    }

    const chatHubConnect = async (hubConnection) => {
        await getGlobalInfo();
        hubConnection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:5050/chathub", {
            accessTokenFactory: () => localStorage.getItem('token'),
        }).build();

        await hubConnection.start();
        setConnection(hubConnection);

        hubConnection.on("ReceiveNewMessage", async (messageDto) => {
            if (messageDto.type != "group") {
                setPrivateChats(chats => {
                    const updatedChats = new Map(chats);
                    var chat = chats.get(messageDto.chatId);
                    chat.messages.set(messageDto.id, messageDto);
                    updatedChats.set(messageDto.chatId, chat);
                    chatsRef.current = updatedChats;
                    if (messageDto.chatId == currentChatRef.current?.id)
                    {
                        setCurrentChat(updatedChats.get(messageDto.chatId));
                        currentChatRef.current = updatedChats.get(messageDto.chatId);
                    }
                    return updatedChats;
                });
                if (messageDto.chatId == currentChatRef.current?.id)
                {
                    await hubConnection.invoke("UserSeenMessages", messageDto.senderId, [messageDto.id]);
                    if (chatScroll.current.clientHeight +  chatScroll.current.scrollTop >= chatScroll.current.scrollHeight - 300)
                        scrollTop();
                    else
                        setNumberOfUnSeenMessages(number => number + 1);
                }
            } else {
                setCurrentUser(user => {
                    const updatedUser = new Object(user);
                    const updatedChat =  updatedUser.groupChats.filter((chat) => chat.id == messageDto.chatId)[0].messages.set(messageDto.id, messageDto);
                    updatedUser.groupChats.filter((chat) => chat.id == messageDto.chatId)[0] = updatedChat;
                    return updatedUser;
                })
                setCurrentChat(currentChat => {
                    const updatedCurrentChat = new Object(currentChat);
                    updatedCurrentChat.messages.set(messageDto.id, messageDto);
                    return updatedCurrentChat;
                });
            }
        });

        hubConnection.on("ReceiveUpdatedMessages", async (messagesDto) => {
            console.log(messagesDto);
            setPrivateChats(chats => {
                const updatedChats = new Map(chats);
                var chat = chats.get(messagesDto[0].chatId);
                for (let i = 0; i < messagesDto.length; i++) {
                    chat.messages.set(messagesDto[i].id, messagesDto[i]);
                }
                updatedChats.set(messagesDto[0].chatId, chat);
                chatsRef.current = updatedChats;
                if (messagesDto[0].chatId == currentChatRef.current?.id)
                {
                    setCurrentChat(updatedChats.get(messagesDto[0].chatId));
                    currentChatRef.current = updatedChats.get(messagesDto[0].chatId);
                }
                return updatedChats;
            });
        });

        hubConnection.on("OnUserOnline", (userId) => {
            setUsers(users => {
                const updatedUsers = new Map(users);
                var user = users.get(userId);
                if (user) 
                {
                    updatedUsers.set(userId, {...user, isOnline: true});
                    
                    if (receiverRef.current)
                        setReceiverUser(updatedUsers.get(receiverRef.current.id));
                }
                return updatedUsers;
            });
        });

        hubConnection.on("OnUserOffline", (userId) => {
            setUsers(users => {
                const updatedUsers = new Map(users);
                var user = users.get(userId);
                if (user) 
                {
                    updatedUsers.set(userId, {...user, isOnline: false});
                    if (receiverRef.current)
                        setReceiverUser(updatedUsers.get(receiverRef.current.id));
                }
                return updatedUsers;
            });
        });

        hubConnection.on("ReceivingTypingStatus", (userId, status) => {
            setUsers(users => {
                const updatedUsers = new Map(users);
                var user = users.get(userId);
                if (user) 
                {
                    updatedUsers.set(userId, {...user, isTyping: status});
                    if (receiverRef.current)
                        setReceiverUser(updatedUsers.get(receiverRef.current.id));
                }
                return updatedUsers;
            });
        });
    }

    const notificationHubConnect = async (notificationHubConnection) => {
        notificationHubConnection = new signalR.HubConnectionBuilder()
                .withUrl("http://localhost:5050/notificationhub", {
                    accessTokenFactory: () => localStorage.getItem('token'),
                })
                .build();
        
        await notificationHubConnection.start();

        notificationHubConnection.on("InfoNotificationReceive", (notification) => {
            console.log(notification);
            const notificationObj = {
                id: notification.id,
                content: notification.content,
                createdAt: notification.createdAt,
                type: notification.type
            }
            setNotifications(infoNotifications => {
                const updatedInfoNotifications = new Map(infoNotifications);
                updatedInfoNotifications.set(notification.id, notificationObj);
                return updatedInfoNotifications;
            })
            setNewNotification(true);
        });

        notificationHubConnection.on("AssociatedNotificationReceive", (notification) => {
            console.log(notification);
            const notificationObj = {
                id: notification.id,
                content: notification.content,
                createdAt: notification.createdAt,
                requestSenderId: notification.requestSenderId,
                requestSenderName: notification.requestSenderName,
                requestSenderPhoto: notification.requestSenderPhoto,
                type: notification.type
            }

            setNotifications(associatedNotifications => {
                const updatedAssociatedNotifications = new Map(associatedNotifications);
                updatedAssociatedNotifications.set(notification.id, notificationObj);
                return updatedAssociatedNotifications;
            });

            setNewNotification(true);
        });
    }

    useEffect(() => {
        let chatHubConnection;
        let notificationHubConnection;
        
        
        chatHubConnect(chatHubConnection);
        notificationHubConnect(notificationHubConnection);

        return () => {
            if (chatHubConnection)
                chatHubConnection.stop();

            if (notificationHubConnection)
                notificationHubConnection.stop();
        }
    }, []);


    useEffect(() => {
        if (currentChat)
            seenMessages(currentChat.users[0].id, Array.from(currentChat.messages.values()));
        currentChatRef.current = currentChat;
        
    }, [currentChat]);


    const scrollTop = () => {
        if (chatScroll.current)
            chatScroll.current.scrollTop = chatScroll.current.scrollHeight - 60;
    }


    // handle user send messages
    const sendMessage = useCallback(async (message, chatUserId, attachmentURL, attachment, type) => {
        const guid = crypto.randomUUID();
        if (connection) {
            const messageObj = {
                id: guid,
                content: message,
                createdAt: new Date().toISOString(),
                senderId: currentUser.id,
                receiverId: chatUserId,
                attachmentPath: attachmentURL,
                senderPhoto: currentUser.photo,
                senderName: currentUser.fullName,
                status: type == "group" ? 1 : 0,
            }
            if (type == "group") {
                setCurrentUser(user => {
                    const updatedUser = new Object(user);
                    const updatedChat =  updatedUser.groupChats.filter((chat) => chat.id == chatUserId)[0].messages.set(messageObj.id, messageObj);
                    updatedUser.groupChats.filter((chat) => chat.id == chatUserId)[0] = updatedChat;
                    return updatedUser;
                })
                setCurrentChat(currentChat => {
                    const updatedCurrentChat = new Object(currentChat);
                    updatedCurrentChat.messages.set(messageObj.id, messageObj);
                    return updatedCurrentChat;
                });
            } else {
                setPrivateChats(chats => {
                    const updatedChats = new Map(chats);
                    var chat = chats.get(currentChat.id);
                    chat.messages.set(messageObj.id, messageObj);
                    updatedChats.set(currentChat.id, chat);
                    chatsRef.current = updatedChats;
                    setCurrentChat(updatedChats.get(currentChat.id));
                    currentChatRef.current = updatedChats.get(currentChat.id);
                    return updatedChats;
                })
            }
            if (currentChatRef.current)
                await onStopTyping(currentChatRef.current.id);

            var URL = "";
            if (attachment != null) {
                const formData = new FormData();
                formData.append("photo", attachment);
                URL = (await UploadMessageAttachment(formData)).attachment;
            }
            
            if (type != "group")
                await connection.invoke("SendMessageToSpecificUser", message, chatUserId, guid, URL);
            else
                await connection.invoke("SendMessageToGroupChat", message, chatUserId, guid, URL);
            scrollTop();
        }
        

    }, [connection, currentChat]);


    const handleAcceptFriendRequest = useCallback(async (senderId, notificationId) => {
        await AcceptFriendRequest(senderId);
        setNotifications(notifications => {
            const updatedNotification = new Map(notifications);
            updatedNotification.delete(notificationId);
            return updatedNotification;
        });
    });

    // handle user seen messages
    const seenMessages = useCallback(async (receiverId, messages) => {
        if (connection) {
            const unSeenMessages = messages
                                        .filter((message) => message.senderId != currentUserRef.current?.id && message.status != 3)
                                        .map((message) => message.id);
            if (unSeenMessages.length > 0)
            {
                await connection.invoke("UserSeenMessages", receiverId, unSeenMessages);
            }
        }
    }, [connection]);


    // handle on typing
    const onStartTyping = useCallback(async (chatId) => {
        if (connection) {
            await connection.send("UserIsTyping", chatId);
        }
    }, [connection]);
    const onStopTyping = useCallback(async (chatId) => {
        if (connection) {
            await connection.send("UserStoppedTyping", chatId);
        }
    }, [connection]);


    const HandleSendingFriendRequest = useCallback(async (receiverId) => {
        if (sentFriendRequests.has(receiverId))
        {
            setSentFriendRequests(friendRequests => {
                const updatedFriendRequests = new Map(friendRequests);
                updatedFriendRequests.delete(receiverId);
                sentFriendRequestsRef.current = updatedFriendRequests;
                return updatedFriendRequests;
            });
            await DeleteFriendRequest(receiverId);
            return;
        }

        const friendRequestObj = {
            receiverId: receiverId,
            status: 1,
            createdAt: new Date()
        }
        setSentFriendRequests(friendRequests => {
            const updatedFriendRequests = new Map(friendRequests);
            updatedFriendRequests.set(receiverId, friendRequestObj);
            sentFriendRequestsRef.current = updatedFriendRequests;
            return updatedFriendRequests;
        });

        await SendFriendRequest(receiverId);
    }, [sentFriendRequests]);

    if (!loading) {
        return (
            <>
              <div className={styles.parent}>
                <LeftNavBar 
                    newNotification={newNotification}
                    setNewNotification={setNewNotification}
                    acceptFriendRequest={handleAcceptFriendRequest}
                    notifications={Array.from(notifications.values())}
                    currentChat={currentChat} 
                    getChatInfo={getChatInfo} 
                    users={Array.from(users.values())} 
                    user={currentUser}
                />

                <RightBox
                    getChatInfo={getChatInfo}
                    sentFriendRequests={sentFriendRequests}
                    handleSendingFriendRequest={HandleSendingFriendRequest}
                    numberOfUnSeenMessages={numberOfUnSeenMessages}
                    setUnSeenMessagesTrigger={setUnSeenMessagesTrigger}
                    unSeenMessagesTrigger={unSeenMessagesTrigger}
                    onStopTyping={onStopTyping}
                    onStartTyping={onStartTyping} 
                    setChatTrigger={setChatTrigger} 
                    chatTrigger={chatTrigger} 
                    scroll={chatScroll} 
                    globalChat={currentChat} 
                    receiverUser={receiverUser} 
                    sendMessage={sendMessage} 
                    currentUser={currentUser}
                />
              </div>
            </>
          );
    } else {
        return (<>
            <div className={styles.logo}>
                <h1>Connectify</h1>
                <img src="/icons/connectify-icon-white.svg" width={40} height={40}/>
             </div>
        </>);
    }
}
