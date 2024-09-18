import styles from "./rightBox.module.css";
import Presentation from "../presentation/presentation.module";
import Chat from "../chat/chat.module";
import GroupChat from "../groupChat/groupChat.module";

export default function RightBox({
                                getChatInfo,
                                sentFriendRequests, 
                                handleSendingFriendRequest,
                                numberOfUnSeenMessages, 
                                setUnSeenMessagesTrigger, 
                                unSeenMessagesTrigger, 
                                onStopTyping, 
                                onStartTyping, 
                                scroll, 
                                sendMessage, 
                                currentUser, 
                                receiverUser, 
                                globalChat, 
                                setChatTrigger, 
                                chatTrigger}) {

    return(<>
        <div className={styles.parent}>
            {receiverUser == null && globalChat == null ? <Presentation /> :
            globalChat == null || globalChat.type != 1 ? <Chat 
                sentFriendRequests={sentFriendRequests}
                handleSendingFriendRequest={handleSendingFriendRequest}
                numberOfUnSeenMessages={numberOfUnSeenMessages} 
                setUnSeenMessagesTrigger={setUnSeenMessagesTrigger} 
                unSeenMessagesTrigger={unSeenMessagesTrigger} 
                onStopTyping={onStopTyping} 
                onStartTyping={onStartTyping} 
                setChatTrigger={setChatTrigger} 
                chatTrigger={chatTrigger} 
                scroll={scroll} 
                globalChat={globalChat} 
                receiverUser={receiverUser} 
                sendMessage={sendMessage} 
                currentUser={currentUser}
            /> : 
            <GroupChat 
                getChatInfo={getChatInfo}
                numberOfUnSeenMessages={numberOfUnSeenMessages}
                setUnSeenMessagesTrigger={setUnSeenMessagesTrigger}
                unSeenMessagesTrigger={unSeenMessagesTrigger}
                onStopTyping={onStopTyping}
                onStartTyping={onStartTyping}
                scroll={scroll}
                sendMessage={sendMessage}
                currentUser={currentUser}
                globalChat={globalChat}
            />}
        </div>
    </>);
}