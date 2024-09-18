export default async function UpdateUser(body) {
    const response = await fetch(`http://localhost:5050/api/v1/users`,{
        method: "PATCH",
        headers: {
            "Authorization": `Bearer ${localStorage.getItem('token')}`,
            "Content-Type": "application/x-www-form-urlencoded",
        },
        credentials: "include",
        body: new URLSearchParams(body),
    });

    return await response.json();
}
