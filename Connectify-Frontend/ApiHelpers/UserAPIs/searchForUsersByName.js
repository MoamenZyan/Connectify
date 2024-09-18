export default async function SearchForUsersByName(name) {
    const response = await fetch(`http://localhost:5050/api/v1/users/search?name=${name}`,{
        method: "GET",
        headers: {
            "Authorization": `Bearer ${localStorage.getItem('token')}`,
        },
        credentials: "include",
    });

    return await response.json();
}