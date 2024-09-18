"use client";
import { useRouter } from "next/navigation";
import styles from "./settingsLeftNav.module.css";

export default function SettingsLeftNav({setIsAccount}) {
    const router = useRouter();
    
    const handleLogout = () => {
        localStorage.removeItem('token');
        router.push("/login");
    }
    return(<>
            <div className={styles.parent}>
                <div onClick={() => {router.push("/")}} className={styles.logo}>
                    <div className={styles.logo_wrapper}>
                        <img src="/icons/connectify-icon-white.svg" width={30} height={30}/>
                        <h1>Connectify</h1>
                    </div>
                </div>
                <div className={styles.tabs}>
                    <div onClick={() => {setIsAccount(true)}} className={styles.tab}>
                        <div>
                            <img src="/icons/profile-icon-white.svg" width={25} height={25}/>
                            <h3>My Account</h3>
                        </div>
                    </div>
                    <div onClick={handleLogout} className={`${styles.tab} ${styles.logout}`}>
                        <div>
                            <img src="/icons/logout-icon-white.svg" width={25} height={25}/>
                            <h3>Logout</h3>
                        </div>
                    </div>
                </div>
            </div>
    </>);
}