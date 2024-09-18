"use client";
import SettingsLeftNav from "@/components/settingsLeftNav/settingsLeftNav.module";
import styles from "./page.module.css";
import { useEffect, useState, useRef } from "react";
import { useRouter } from "next/navigation";
import GetCurrentUserInfo from "@/ApiHelpers/UserAPIs/getCurrentUserInfo";
import Presentation from "@/components/presentation/presentation.module";
import UploadUserPhoto from "@/ApiHelpers/UserAPIs/UploadUserPhoto";
import UpdateUser from "@/ApiHelpers/UserAPIs/UpdateUser";

export default function SettingsPage() {
    const input = useRef(null);
    const [user, setUser] = useState(null);
    const [isAccount, setIsAccount] = useState(false);
    const [isEdit, setIsEdit] = useState(false);
    const router = useRouter();
    
    const [photoUrl, setPhotoUrl] = useState(null);
    const [photo, setPhoto] = useState(null);
    const [firstName, setFirstName] = useState();
    const [lastName, setLastName] = useState();
    const [email, setEmail] = useState();
    const [phone, setPhone] = useState();

    const [saved, setSaved] = useState(false);
    const [errorMessage, setErrorMessage] = useState(null);

    useEffect(() => {
        const getInfo = async () => {
            const result = await GetCurrentUserInfo(localStorage.getItem('token'));
            if (result.status)
            {
                setUser(result.user);
                setFirstName(new String(result.user.fullName).slice(0, new String(result.user.fullName).indexOf(" ")));
                setLastName(new String(result.user.fullName).slice(new String(result.user.fullName).indexOf(" ")));
                setEmail(result.user.email);
                setPhone(result.user.phone);
            }
            else
            {
                router.push("/login");
            }
        }

        getInfo();
    }, [saved]);

    const handlePhoto = () => {
        var file = input.current.files[0];
        setPhotoUrl(URL.createObjectURL(file));
        setPhoto(file);
        setIsEdit(true);
    }

    const handleSave = async () => {
        if (photo != null)
        {
            var photoForm = new FormData();
            photoForm.append("photo", photo);
            await UploadUserPhoto(photoForm);
            setSaved(!saved);
        }
        var infoForm = new FormData();
        (new String(user.fullName).slice(0, new String(user.fullName).indexOf(" ")) != firstName )&& infoForm.append("Fname", firstName);
        (new String(user.fullName).slice(new String(user.fullName).indexOf(" ")) != lastName) && infoForm.append("Lname", lastName);
        (user.email != email) && infoForm.append("Email", email);
        (user.phone != phone) && infoForm.append("Phone", phone);

        var result = await UpdateUser(infoForm);
        if (!result.status)
        {
            setErrorMessage(result.message);
            return;
        }

        setSaved(!saved);
        setErrorMessage(null);
    }

    if (user) {
        return(<>
            <div className={styles.parent}>
                <SettingsLeftNav setIsAccount={setIsAccount} />
                <div className={styles.right}>
                    {isAccount == false && <Presentation />}
                    {isAccount && <div className={styles.user}>
                        <div className={styles.first}>
                            <h3 className={"sans-text"}>My Account</h3>
                        </div>
                        <div className={styles.account_box}>
                            <div className={styles.user_info}>
                                <div className={styles.photos}>
                                    {photoUrl == null ? <img className={styles.user_photo} src={user.photo == "" ? "./icons/profile-icon-white.svg" : user.photo}/>:
                                    <img className={styles.user_photo} src={photoUrl}/>}
                                    <img onClick={() => {input.current.click()}} className={styles.add_photo} src="./icons/addphoto-icon-white.svg"/>
                                    <input onChange={handlePhoto} ref={input} type="file" style={{display: "none"}}/>
                                </div>
                                <div className={styles.last}>
                                    <div>
                                        <h5 className={"sans-text"}>{user.fullName}</h5>
                                        <p>Your Status</p>
                                    </div>
                                    {!isEdit && <button onClick={() => {setIsEdit(true)}}>Edit User Profile</button>}
                                </div>
                            </div>
                            <div className={styles.infos}>
                                <div className={styles.info}>
                                    <h4>First Name</h4>
                                    {isEdit ? <input type="text" placeholder="Enter your first name" value={firstName} onChange={(e) => setFirstName(e.target.value)}/> :
                                    <p>{new String(user.fullName).slice(0, new String(user.fullName).indexOf(" "))}</p> }
                                    
                                </div>
                                <div className={styles.info}>
                                    <h4>Last Name</h4>
                                    {isEdit ? <input type="text" placeholder="Enter your last name" value={lastName} onChange={(e) => setLastName(e.target.value)}/> :
                                    <p>{new String(user.fullName).slice(new String(user.fullName).indexOf(" "))}</p>}
                                </div>
                                <div className={styles.info}>
                                    <h4>Email</h4>
                                    {isEdit ? <input type="text" placeholder="Enter your email" value={email} onChange={(e) => setEmail(e.target.value)}/> :
                                    <p>{user.email}</p>}
                                </div>
                                <div className={styles.info}>
                                    <h4>Phone</h4>
                                    {isEdit ? <input type="text" placeholder="Enter your phone" value={phone} onChange={(e) => setPhone(e.target.value)}/> :
                                    <p>{user.phone}</p>}
                                </div>
                            </div>
                            {isEdit && <div className={styles.buttons}>
                                <button className={styles.cancel} onClick={async() => {
                                setIsEdit(false);
                                setPhotoUrl(null);
                                setPhoto(null);
                                }}>Cancel</button>
                                <button className={styles.save} onClick={async() => {
                                await handleSave();
                                setIsEdit(false);
                                }}>Save</button>
                            </div>}
                            {errorMessage != null && <p className={styles.error}>{errorMessage}</p>}
                        </div>
                    </div>}
                </div>
            </div>
        </>);
    }
}