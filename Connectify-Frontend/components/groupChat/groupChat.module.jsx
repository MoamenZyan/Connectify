"use client";
import { useState, useEffect } from "react";
import { useRef } from "react";
import Message from "../message/message.module";
import EmojiPicker from "emoji-picker-react";
import styles from "./groupChat.module.css";
import GroupPropertiesList from "../groupPropertiesList/groupPropertiesList.module";



export default function GroupChat({
                            getChatInfo,
                            numberOfUnSeenMessages,
                            setUnSeenMessagesTrigger, 
                            unSeenMessagesTrigger, 
                            onStopTyping, onStartTyping, 
                            scroll,
                            sendMessage,
                            currentUser,
                            globalChat}) {
    
    const [message, setMessage] = useState("");
    const [emojiPicker, setEmojiPicker] = useState(false);
    const [messageAttachment, setMessageAttachment] = useState(null);
    const [messagePhoto, setMessagePhoto] = useState(null);
    const [groupPropertiesList, setGroupPropertiesList] = useState(false);
    
    const photoRef = useRef();
    const typingTimeoutRef = useRef(null);
    const textareaRef = useRef(null);
    const container = useRef(null);
    const input = useRef(null);

    const adjustHeight = () => {
        const textarea = textareaRef.current;
        textarea.style.height = 'auto';
        const newHeight = textarea.scrollHeight;
        textarea.style.height = "65px";
        container.current.style.height = "65px";
    };

    useEffect(() => {
        scroll.current.scrollTop = scroll.current.scrollHeight - scroll.current.clientHeight;
        scroll.current.addEventListener('scroll', () => {
            if (scroll.current.clientHeight + scroll.current.scrollTop >= scroll.current.scrollHeight - 100 &&
                    numberOfUnSeenMessages > 0)
                setUnSeenMessagesTrigger(!unSeenMessagesTrigger);
        });
        const textarea = textareaRef.current;
        if (globalChat == null)
            textarea.disabled = true;

        textarea.addEventListener('input', () => {
            if (photoRef.current == null) {
                const textarea = textareaRef.current;
                textarea.style.height = 'auto';
                const newHeight = textarea.scrollHeight;
                textarea.style.height = `${newHeight}px`;
                container.current.style.height = `${newHeight}px`;
            }
        });
        adjustHeight();
        return () => {
            textarea.removeEventListener('input', () => {
                if (photoRef.current == null) {
                    const textarea = textareaRef.current;
                    textarea.style.height = 'auto';
                    const newHeight = textarea.scrollHeight;
                    textarea.style.height = `${newHeight}px`;
                    container.current.style.height = `${newHeight}px`;
                }
            });
            if (scroll.current)
                scroll.current.removeEventListener('scroll', () => {
                    if (scroll.current.clientHeight + scroll.current.scrollTop >= scroll.current.scrollHeight - 100 &&
                            numberOfUnSeenMessages > 0)
                        setUnSeenMessagesTrigger(!unSeenMessagesTrigger);
                });
        };
    }, []);

    const handleMessageAttachment = () => {
        const photo = input.current.files[0];
        container.current.style.height = "200px";
        setMessagePhoto(URL.createObjectURL(photo));
        setMessageAttachment(photo);
        photoRef.current = URL.createObjectURL(photo);
    }
        
        return (<>
            <div className={styles.parent}>
                <div className={styles.chat_header}>
                    <div className={styles.user_info}>
                        <img className={styles.group_photo} src={globalChat.photo == "" ? "/icons/groups-icon-white.svg" : globalChat.photo} alt="" width={60} height={60} />
                        <h3 className={"sans-text"}>{globalChat.name}</h3>
                    </div>
                    <div><img onClick={() => {setGroupPropertiesList(!groupPropertiesList)}} src="./icons/dots-icon-white.svg" width={25} height={25}/></div>
                    {groupPropertiesList && <GroupPropertiesList globalChat={globalChat} getChatInfo={getChatInfo} group={globalChat}/>}
                </div>
                <div ref={scroll} className={styles.content}>
                    <div className={styles.encrypt_declaration}>
                        <p className={"mono-text"}>Messages are end-to-end encrypted. No one outside of this chat, not even Connectify, can read or listen to them.</p>
                    </div>
                    {globalChat && <div className={styles.messages}>
                        {Array.from(globalChat.messages.values()).sort((a, b) => {return new Date(a.createdAt) - new Date(b.createdAt);}).map((message) => (
                            <Message key={message.id} chatType={globalChat == null ? 0 : globalChat.type} message={message} currentUserId={currentUser.id}/>
                        ))}
                    </div>}
                    {numberOfUnSeenMessages > 0 && <div onClick={() => {setUnSeenMessagesTrigger(!unSeenMessagesTrigger)}} className={styles.unSeenMessages}>
                        <span>{numberOfUnSeenMessages}</span>
                    </div>}
                </div>
                <div className={styles.chat_footer}>
                    <div className={styles.input_wrapper}>
                        {photoRef.current != null && <div className={styles.photo_attachment_div}>
                                <div className={styles.attachment_wrapper}>
                                    <img src={messagePhoto}/>
                                    <img onClick={() => {
                                        setMessageAttachment(null);
                                        setMessagePhoto(null);
                                        photoRef.current = null;
                                        adjustHeight();
                                    }} src={"./icons/close-icon-white.svg"}/>
                                </div>
                            </div>}
                        <div style={{opacity: globalChat == null ? "0.3" : "1"}} ref={container} className={styles.input}>
                            <textarea ref={textareaRef} value={message} onChange={(e) => {
                                setMessage(e.target.value);
                                if (typingTimeoutRef.current)
                                    clearTimeout(typingTimeoutRef.current);
                                
                                if (globalChat)
                                    onStartTyping(globalChat.id);

                                typingTimeoutRef.current = setTimeout(() => {
                                    if (globalChat)
                                        onStopTyping(globalChat.id);
                                }, 3000);       
                            }} placeholder="Type a message"/>
                            <div className={styles.icons}>
                                <img onClick={() => {input.current.click()}} src="./icons/attach-icon-white.svg" width={30} height={30}/>
                                <img onClick={() => {setEmojiPicker(!emojiPicker)}} src="./icons/emoji-icon-white.svg" width={25} height={25}/>
                            </div>
                            <input onChange={handleMessageAttachment} ref={input} type="file" style={{display: "none"}} />
                            {emojiPicker && <EmojiPicker className={styles.emoji_picker} style={{
                                    position: "absolute",
                                    top: "-410px",
                                    right: "-30px",
                                    backgroundColor: "rgba(35, 50, 93, 0.719)",
                                    height: "400px",
                                    width: "100%",
                                    maxWidth: "300px"}}
                                    lazyLoadEmojis={true}
                                    allowExpandReactions={false}
                                    onEmojiClick={(e) => {setMessage(message => message + e.emoji)}}
                                    />}
                        </div>
                    </div>
                    <div onClick={async () => {
                        await sendMessage(message, globalChat.id, photoRef.current, messageAttachment, "group");
                        setMessage("");
                        }} style={{opacity: globalChat == null ? "0.3" : "1"}} className={styles.send}>
                        <img src="./icons/send-icon-white.svg" width={35} height={35}/>
                    </div>
                </div>
            </div>
        </>)
}
