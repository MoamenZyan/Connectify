export default async function DeleteFriendRequest(receiverId) {
    const response = await fetch(`https://e228-197-48-180-182.ngrok-free.app/api/v1/users/friend-request/${receiverId}`,{
        method: "DELETE",
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
