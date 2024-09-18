export default async function UploadUserPhoto(body) {
    const response = await fetch(`http://localhost:5050/api/v1/users/upload-photo`,{
        method: "POST",
        headers: {
            "Authorization": `Bearer ${localStorage.getItem('token')}`,
        },
        credentials: "include",
        body: body
    });

    if (response.ok)
        return true;
    else
        return false;
}
