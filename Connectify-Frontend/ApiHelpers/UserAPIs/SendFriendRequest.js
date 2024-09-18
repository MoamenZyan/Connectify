export default async function SendFriendRequest(receiverId) {
    const response = await fetch(`http://localhost:5050/api/v1/users/friend-request/${receiverId}`,{
        method: "POST",
        headers: {
            "Authorization": `Bearer ${localStorage.getItem('token')}`,
        },
        credentials: "include",
    });

    if (response.ok)
        return true;
    else
        return false;
}
