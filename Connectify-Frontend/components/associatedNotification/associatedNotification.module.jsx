import styles from "./associatedNotification.module.css";

export default function AssociatedNotification({notification}) {
    return (<>
        <div className={styles.notification}>
            <div>
                <img src={notification.requestSenderPhoto}/>
            </div>
            <div>
                <p className={"mono-text"}>{notification.content}</p>
                <span>{new Date(notification.createdAt).toLocaleTimeString([], { hour: 'numeric', minute: '2-digit', hour12: true })}</span>
            </div>
        </div>
    </>);
}