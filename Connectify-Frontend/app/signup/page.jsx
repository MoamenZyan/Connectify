"use client";
import styles from "./page.module.css";
import { useState } from "react";
import { useRouter } from "next/navigation";
import RegisterNewUser from "@/ApiHelpers/UserAPIs/registerNewUser";

export default function SignupPage() {
    const [isPassword, setIsPassword] = useState(true);
    const [fname, setFname] = useState("");
    const [lname, setLname] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [phone, setPhone] = useState("");
    const [error, setError] = useState(false);
    const [errorMessage, setErrorMessage] = useState("");
    const router = useRouter();

    const RegisterNewUserHandler = async () => {
        const form = new FormData();
        form.append("Fname", fname);
        form.append("Lname", lname);
        form.append("Email", email);
        form.append("Password", password);
        form.append("Phone", phone);
        const result = await RegisterNewUser(form);
        if (result.status == true)
            router.push("/login")
        else
            setError(true);
        setErrorMessage(result.message);
    }

    return (<>
        <div className={styles.parent}>
            <div className={styles.form}>
                <div className={styles.left}>
                    <h1 className={"sans-text"}>Signup</h1>
                    <div className={styles.input_div}>
                        <img src="./icons/username-icon-white.svg" width={25} height={25}/>
                        <input value={fname} onChange={(e) => {setFname(e.target.value)}} className={"mono-text"} placeholder="Enter your first name"/>
                    </div>
                    <div className={styles.input_div}>
                        <img src="./icons/username-icon-white.svg" width={25} height={25}/>
                        <input value={lname} onChange={(e) => {setLname(e.target.value)}} className={"mono-text"} placeholder="Enter your last name"/>
                    </div>
                    <div className={styles.input_div}>
                        <img src="./icons/email-icon-white.svg" width={25} height={25}/>
                        <input value={email} onChange={(e) => {setEmail(e.target.value)}} className={"mono-text"} placeholder="Enter your email"/>
                    </div>
                    <div className={styles.input_div}>
                        <img src="./icons/password-icon-white.svg" width={25} height={25}/>
                        <input value={password} onChange={(e) => {setPassword(e.target.value)}} type={isPassword ? "password" : "text"} className={"mono-text"} placeholder="Enter your password"/>
                        <img onClick={() => {setIsPassword(!isPassword)}} className={styles.password} src={`./icons/${isPassword ? "eye" : "eyeoff"}-icon-white.svg`} width={20} height={20}/>
                    </div>
                    <div className={styles.input_div}>
                        <img src="./icons/phone-icon-white.svg" width={25} height={25}/>
                        <input value={phone} onChange={(e) => {setPhone(e.target.value)}} className={"mono-text"} placeholder="Enter your phone"/>
                    </div>
                    {error && <p  className={`${styles.error} mono-text`}>{errorMessage}</p>}
                    <div onClick={RegisterNewUserHandler} className={styles.login_button}>
                        <h3 className={"sans-text"}>Join Us!</h3>
                    </div>
                    <p className={`mono-text ${styles.account}`}>Already have an account? <span onClick={() => {router.push("/login")}}>Login!</span></p>
                </div>
                <div className={styles.right}>
                    <img src="./icons/connectify-icon-white.svg" width={55} height={55}/>
                    <h2 className={"sans-text"}>Connectify</h2>
                    <p className={"mono-text"}>Join a vibrant community, unlock seamless connections, and take the first step toward meaningful conversations with Connectify.</p>
                    <div className={styles.presentation}>
                        <div>
                            <img src="./icons/message-icon-white.svg" width={40} height={40}/>
                            <h2 className={"sans-text"}>Chats</h2>
                            <p className={"mono-text"}>Connect with friends and colleagues instantly.</p>
                        </div>
                        <div>
                            <img src="./icons/groups-icon-white.svg" width={40} height={40}/>
                            <h2 className={"sans-text"}>Groups</h2>
                            <p className={"mono-text"}>Create and manage group chats with ease.</p>
                        </div>
                        <div>
                            <img src="./icons/privacy-icon-white.svg" width={40} height={40}/>
                            <h2 className={"sans-text"}>Privacy</h2>
                            <p className={"mono-text"}>Your conversations are private and secure.</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </>);
}