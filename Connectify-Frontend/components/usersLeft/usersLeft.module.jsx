import styles from "./usersLeft.module.css";
export default function UsersLeft({currentChat, users, getChatInfo}) {
    return (<>
        {users && users.map((user) => (
            <div onClick={async () => {
                if (currentChat == null || currentChat.users[0].id != user.id )
                    await getChatInfo(user, "user");
                
                }} key={user.id} className={styles.tab}>
                <div>
                    <img src={user.photo == "" ? "/icons/profile-icon-white.svg" : user.photo} width={25} height={25}/>
                    <h3>{user.fullName}</h3>
                </div>
            </div>
        ))}
    </>)
}