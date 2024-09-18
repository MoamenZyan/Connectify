import styles from "./infoNotification.module.css"

export default function InfoNotification({notification}) {
    return (<>
            <div className={styles.notification}>
                 <p className={"mono-text"}>{notification.content}</p>
                <span>{new Date(notification.createdAt).toLocaleTimeString([], { hour: 'numeric', minute: '2-digit', hour12: true })}</span>
             </div>
    </>)
}