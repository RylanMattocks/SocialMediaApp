const url = 'http://localhost:5001/api/User/';

export const getUserLogin = async (username) => {
    try {
        const response = await fetch(`${url}login/${username}`);
        return await response.json();
    } catch {
        console.error("Error fetching user");
        return null;
    }
}

export const getUsernameById = async (userId) => {
    try {
        const response = await fetch(`${url}${userId}`);
        const data = await response.json();
        return data.username;
    } catch {
        console.error("Error fetching user");
        return null;
    }
}

export const createUser = async (username, email, profileImageUrl) => {
    try {
        const response = await fetch(`${url}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({username, email, profileImageUrl})
        });
        if (!response.ok) throw new Error();
        return "created";
    } catch {
        console.error("Error creating user");
        return null;
    }
}

export const getUsers = async (userIds) => {
    try {
        const responses = await Promise.all(
            userIds.map(id => 
                fetch(`${url}${id}`)
            )
        ) ;
        const data = await Promise.all(responses.map(res => {
            if (!res.ok) throw new Error();
            return res.json();
        }))
        return data;
    } catch {
        console.error("Error Getting Posts");
        return null;
    }
}
