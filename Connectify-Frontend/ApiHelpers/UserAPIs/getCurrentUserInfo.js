export default async function GetCurrentUserInfo(token) {
    const response = await fetch("http://localhost:5050/api/v1/users/current",{
        method: "GET",
        headers: {
            "Authorization": `Bearer ${token}`,
        },
        credentials: "include"
    });

    if (response.ok)
        return await response.json();
    else
        return false;
}