export default async function AcceptFriendRequest(senderId) {
    const response = await fetch(`http://localhost:5050/api/v1/users/accept-friend-request/${senderId}`,{
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
