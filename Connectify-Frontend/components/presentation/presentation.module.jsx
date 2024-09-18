import styles from "./presentation.module.css";

export default function Presentation() {
    return (<>
        <div className={styles.parent}>
            <div className={styles.center}>
                <img src="./icons/connectify-icon-white.svg" width={60} height={60}/>
                <h1>Connectify</h1>
                <p>Delve into profound perspectives, participate in enirching dialogues, and discover fresh opportunities with Connectify.</p>
            </div>
            <div className={styles.grid}>
                <div className={styles.card}>
                    <img src="./icons/message-icon-white.svg" width={40} height={40}/>
                    <h2>Chats</h2>
                    <div className={styles.info}>
                        <p>Connect with friends and colleagues instantly.</p>
                        <p>Engage in real-time conversations with anyone, anytime.</p>
                        <p>Send text, images, and videos seamlessly within chats.</p>
                        <p>Stay connected with friends through instant messaging.</p>
                    </div>
                </div>
                <div className={styles.card}>
                    <img src="./icons/groups-icon-white.svg" width={40} height={40}/>
                    <h2>Groups</h2>
                    <div className={styles.info}>
                        <p>Create and manage group chats with ease.</p>
                        <p>Collaborate with friends or teams in dedicated group chats.</p>
                        <p>Bring people together in group conversations, all in one place.</p>
                        <p>Chat with multiple people at once, in real-time group chats.</p>
                    </div>
                </div>
                <div className={styles.card}>
                    <img src="./icons/privacy-icon-white.svg" width={40} height={40}/>
                    <h2>Privacy</h2>
                    <div className={styles.info}>
                        <p>Your conversations are private and secure.</p>
                        <p>Enjoy peace of mind with end-to-end encrypted messages.</p>
                        <p>Your personal data is protected with advanced security measures.</p>
                        <p>Control who can see your messages and stay in charge of your privacy.</p>
                    </div>
                </div>
            </div>
        </div>
    </>)
}