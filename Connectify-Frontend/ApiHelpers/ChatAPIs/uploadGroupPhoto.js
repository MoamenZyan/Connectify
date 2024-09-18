export default async function UploadGroupPhoto(formData) {
    const response = await fetch(`http://localhost:5050/api/v1/chats/upload-group-photo`,{
        method: "POST",
        headers: {
            "Authorization": `Bearer ${localStorage.getItem('token')}`,
        },
        credentials: "include",
        body: formData
    });

    if (response.ok)
        return await response.json();
    else 
        return false;
}