export default async function LoginUser(credentials) {
    const response = await fetch("http://localhost:5050/api/v1/users/login",{
        method: "POST",
        headers: {
            "Content-Type": "application/x-www-form-urlencoded",
            "Authorization": `Basic ${btoa(credentials)}`,
        },
        credentials: "include",
    });

    return await response.json();
}