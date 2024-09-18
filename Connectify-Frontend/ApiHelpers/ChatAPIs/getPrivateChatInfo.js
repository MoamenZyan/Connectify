export default async function GetPrivateChatInfo(receiverId) {
    const response = await fetch(`http://localhost:5050/api/v1/chats/${receiverId}`,{
        method: "GET",
        headers: {
            "Authorization": `Bearer ${localStorage.getItem('token')}`,
        },
        credentials: "include"
    });

    if (response.ok)
        return await response.json();
    else 
        return false;
}