"use client";
import { set } from "lodash";
import styles from "./groupCreation.module.css";
import { useRef, useState } from "react";

import CreateNewGroup from "@/ApiHelpers/ChatAPIs/createGroupChat";
import UploadGroupPhoto from "@/ApiHelpers/ChatAPIs/uploadGroupPhoto";

export default function GroupCreation({users}) {
    const input = useRef(null);
    const [groupPhoto, setGroupPhoto] = useState(null);
    const [groupPhotoURL, setGroupPhotoURL] = useState(null);
    const [checkedUsers, setCheckedUsers] = useState(new Set());
    const [groupName, setGroupname] = useState("");

    const handleGroupPhoto = () => {
        const photo = input.current.files[0];
        setGroupPhoto(photo);
        setGroupPhotoURL(URL.createObjectURL(photo));
    }

    const handleUserSelection = (userId) => {
        const checkbox = document.getElementById(`checkbox-${userId}`);
        if (checkbox.checked) {
            checkbox.checked = false;
            setCheckedUsers(checkedUsers => {
                const updatedCheckedUsers = new Set(checkedUsers);
                updatedCheckedUsers.delete(userId);
                return updatedCheckedUsers;
            });
        } else {
            checkbox.checked = true;
            setCheckedUsers(checkedUsers => {
                const updatedCheckedUsers = new Set(checkedUsers);
                updatedCheckedUsers.add(userId);
                return updatedCheckedUsers;
            });
        }
    }


    const handleGroupCreation = async() => {
        const formData = new FormData();
        var photoUrl = "";
        if (groupPhoto) {
            formData.append("Photo", groupPhoto);
           var url = await UploadGroupPhoto(formData);
           photoUrl = url.attachment;
        }
        formData.append("Name", groupName);
        formData.append("Description", "");
        formData.append("Photo", photoUrl);
        formData.append("MembersIds", Array.from(checkedUsers.values()));
        await CreateNewGroup(formData);
    }

    return (<>
        <div className={styles.parent}>
            <h4 className={"sans-text"}>Create New Group</h4>
            <label className={"sans-text"}>Group Photo</label>
            <div className={styles.group_photo}>
                <img onClick={() => {input.current.click()}} src={groupPhotoURL == null ? "/icons/groups-icon-white2.svg" : groupPhotoURL} width={50} height={50}/>
                <input onChange={handleGroupPhoto} ref={input} style={{display: "none"}} type="file"/>
            </div>

            <label className={"sans-text"}>Group Name</label>
            <input value={groupName} onChange={(e) => {setGroupname(e.target.value)}} className={`${"mono-text"} ${styles.text_input}`} type="text" placeholder="Enter Group Name"/>
            
            <label className={"sans-text"}>Add Users</label>
            <div className={styles.users}>
                {users && users.map((user) => (
                    <div onClick={() => {handleUserSelection(user.id)}} key={user.id} className={styles.tab}>
                        <div>
                            <img src={user.photo == "" ? "/icons/profile-icon-white.svg" : user.photo} width={25} height={25}/>
                            <h3>{user.fullName}</h3>
                        </div>
                        <div>
                            <input type="checkbox" id={`checkbox-${user.id}`} name={user.id} />
                        </div>
                    </div>
                ))}
            </div>
            <div className={styles.buttons}>
                <button onClick={handleGroupCreation} className={"mono-text"}>Create</button>
            </div>
        </div>
    </>)
}