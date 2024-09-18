import styles from "./notificationModal.module.css";
import InfoNotification from "../infoNotification/infoNotification.module";
import FriendRequestNotification from "../friendRequestNotification/friendRequestNotification.module";
import AssociatedNotification from "../associatedNotification/associatedNotification.module";
export default function NotificationModal({acceptFriendRequest, notifications}) {
    return (<>
        <div className={styles.parent}>
            <h3 className={"sans-text"}>Notifications</h3>
            <div className={styles.container}>
                {notifications && notifications.map((notification) => (
                    notification.type == 0 ? <InfoNotification notification={notification}/> : 
                    notification.type == 1 ? <FriendRequestNotification acceptFriendRequest={acceptFriendRequest} notification={notification}/> :
                    <AssociatedNotification notification={notification}/>
                ))}
            </div>
        </div>
    </>);
}