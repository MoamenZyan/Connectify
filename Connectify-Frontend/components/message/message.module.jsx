"use client";
import { useEffect, useState } from "react";
import styles from "./message.module.css";

export default function Message({message, currentUserId, chatType}) {
    const [messageStatus, setMessageStatus] = useState("./icons/sent-icon-white.svg");
    useEffect(() => {
        if (message.status == 0)
            setMessageStatus("./icons/sending-icon-white.svg");

        if (message.status == 1)
            setMessageStatus("./icons/saved-icon-white.svg");

        if (message.status == 2)
            setMessageStatus("./icons/sent-icon-white.svg");

        if (message.status == 3)
            setMessageStatus("./icons/seen-icon-green.svg");

    }, [message]);
    return (<>
        <div className={`${message.senderId == currentUserId ? styles.message_box_mine : styles.message_box_other}`}>
            {chatType != 0 && <div className={styles.profile_photo}>
                <img src={message.senderPhoto} alt="" width={50} height={50}/>
            </div>}
            <div className={`${message.senderId == currentUserId ? styles.message_content_mine : styles.message_content_other} ${styles.message_content}`}>
                {message.attachmentPath != "" && <div className={styles.attachment_div}>
                    <img className={styles.attachment} src={message.attachmentPath}/>
                </div>}
                <pre className={"mono-text"}>{message.content}</pre>
                <span className={styles.message_info}>{new Date(message.createdAt).toLocaleTimeString([], { hour: 'numeric', minute: '2-digit', hour12: true })} {message.senderId == currentUserId && <img src={messageStatus} width={15} height={15}/>}</span>
            </div>
        </div>
    </>)
}