import styles from "./groups.module.css";

export default function Groups({currentChat, groups, getChatInfo}) {
    return (<>
        {groups && groups.map((group) => (
            <div onClick={async () => {
                if (currentChat == null || currentChat.id != group.id )
                    await getChatInfo(group, "group");
                }} key={group.id} className={styles.tab}>
                <div>
                    <img src={group.photo == "" ? "/icons/groups-icon-white.svg" : group.photo} width={25} height={25}/>
                    <h3>{group.name}</h3>
                </div>
            </div>
        ))}
    </>)
}