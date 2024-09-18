export default async function RegisterNewUser(body) {
    const response = await fetch("http://localhost:5050/api/v1/users",{
        method: "POST",
        headers: {
            "Content-Type": "application/x-www-form-urlencoded",
        },
        credentials: "include",
        body: new URLSearchParams(body)
    });

    return await response.json();
}