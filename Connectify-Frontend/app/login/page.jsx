"use client";
import styles from "./page.module.css";
import { useState } from "react";
import { useRouter } from "next/navigation";
import LoginUser from "@/ApiHelpers/UserAPIs/LoginUser";

export default function LoginPage() {
    const [isPassword, setIsPassword] = useState(true);
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const router = useRouter();

    const loginHandler = async () => {
        const credentials = `${email}:${password}`;
        const result = await LoginUser(credentials);
        if (result.status == true)
        {
            localStorage.setItem("token", result.token);
            router.push("/");
        }
    }

    return (<>
        <div className={styles.parent}>
            <div className={styles.form}>
                <div className={styles.left}>
                    <h1 className={"sans-text"}>Login</h1>
                    <div className={styles.input_div}>
                        <img src="./icons/email-icon-white.svg" width={25} height={25}/>
                        <input value={email} onChange={(e) => {setEmail(e.target.value)}} className={"mono-text"} placeholder="Enter your email"/>
                    </div>
                    <div className={styles.input_div}>
                        <img src="./icons/password-icon-white.svg" width={25} height={25}/>
                        <input value={password} onChange={(e) => {setPassword(e.target.value)}} type={isPassword ? "password" : "text"} className={"mono-text"} placeholder="Enter your password"/>
                        <img onClick={() => {setIsPassword(!isPassword)}} className={styles.password} src={`./icons/${isPassword ? "eye" : "eyeoff"}-icon-white.svg`} width={20} height={20}/>
                    </div>
                    <p className={`mono-text ${styles.forget}`}>forgot password?</p>
                    <div onClick={loginHandler} className={styles.login_button}>
                        <h3 className={"sans-text"}>Login Now!</h3>
                    </div>
                    <p className={`mono-text ${styles.account}`}>Doesn't have an account? <span onClick={() => {router.push("/signup")}}>Signup!</span></p>
                </div>
                <div className={styles.right}>
                    <img src="./icons/connectify-icon-white.svg" width={55} height={55}/>
                    <h2 className={"sans-text"}>Connectify</h2>
                    <p className={"mono-text"}>Delve into profound perspectives, participate in enirching dialogues, and discover fresh opportunities with Connectify.</p>
                </div>
            </div>
        </div>
    </>);
}