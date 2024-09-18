export default async function UploadMessageAttachment(form) {
    const response = await fetch(`http://localhost:5050/api/v1/messages/upload-attachment`,{
        method: "POST",
        headers: {
            "Authorization": `Bearer ${localStorage.getItem('token')}`,
        },
        credentials: "include",
        body: form
    });

    if (response.ok)
        return await response.json();
    else 
        return false;
}