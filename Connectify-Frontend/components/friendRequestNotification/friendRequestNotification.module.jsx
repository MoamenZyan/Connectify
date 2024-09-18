import styles from "./friendRequestNotification.module.css";

export default function FriendRequestNotification({acceptFriendRequest, notification}) {
    return (<>
        <div className={styles.notification}>
            <div>
                <img src={notification.requestSenderPhoto}/>
            </div>
            <div>
                <p className={"mono-text"}>{notification.content}</p>
                <span>{new Date(notification.createdAt).toLocaleTimeString([], { hour: 'numeric', minute: '2-digit', hour12: true })}</span>
            </div>
            <div className={styles.buttons}>
                <button onClick={() => acceptFriendRequest(notification.requestSenderId, notification.id)} className={"mono-text"}>Accept</button>
                <button className={"mono-text"}>Reject</button>
            </div>
        </div>
    </>);
}