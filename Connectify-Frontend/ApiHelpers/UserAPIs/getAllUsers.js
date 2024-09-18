export default async function GetAllUsers() {
    const response = await fetch("http://localhost:5050/api/v1/users/all",{
        method: "GET",
        headers: {
            "Authorization": `Bearer ${localStorage.getItem('token')}`,
        },
        credentials: "include"
    });

    return await response.json();
}