export default async function CreateNewGroup(form) {
    const response = await fetch(`http://localhost:5050/api/v1/chats`,{
        method: "POST",
        headers: {
            "Authorization": `Bearer ${localStorage.getItem('token')}`,
        },
        credentials: "include",
        body: new URLSearchParams(form)
    });

    if (response.ok)
        return await response.json();
    else 
        return false;
}