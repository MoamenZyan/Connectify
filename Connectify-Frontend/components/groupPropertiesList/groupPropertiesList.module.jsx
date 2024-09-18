
import styles from "./groupPropertiesList.module.css";
export default function GroupPropertiesList({group, getChatInfo, globalChat}) {
    return (<>
        <div className={styles.parent}>
            <h3 className={"sans-text"}>Group Details</h3>
            <h4 className={"sans-text"}>Description:</h4>
            <p className={"mono-text"}>{group.description}</p>
            <div className={styles.users}>
                <h4 className={"sans-text"}>Group Members:</h4>
                <div className={styles.members}>
                    {group.users.map((user) => (
                        <div onClick={async () => {
                            await getChatInfo(user, "user");
                            }} key={user.id} className={styles.tab}>
                            <div>
                                <img src={user.photo == "" ? "/icons/profile-icon-white.svg" : user.photo} width={25} height={25}/>
                                <h3>{user.fullName}</h3>
                            </div>
                            {user.id == group.ownerId && <div className={`${styles.owner} ${"sans-text"}`}>Owner</div>}
                        </div>
                    ))}
                </div>
            </div>
        </div>
    </>);
}