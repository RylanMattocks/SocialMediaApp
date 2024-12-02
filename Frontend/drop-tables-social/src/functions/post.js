const url = 'https://droptables.azurewebsites.net/api/Post/';

export const createPost = async (userId, content, imageURL) => {
    try {
        const response = await fetch(`${url}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ userId, content, imageURL })
        });
        if (!response.ok) throw new Error();
        return "Created";
    } catch {
        console.error("Error Creating Post");
        return null;
    }
}

export const getPosts = async (userIds) => {
    try {
        const response = await fetch(`${url}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
            params: {
                userIds: userIds.join(','),
                excludeCurrentUser: true
            }
        });
        if (!response.ok) throw new Error();
        return response.json();
    } catch {
        console.error("Error Getting Posts");
        return null;
    }
}

export const getPost = async (postId) => {
    try {
        const response = await fetch(`${url}${postId}`);
        if (!response.ok) throw new Error();
        return response.json();
    } catch {
        console.error("Error Getting Post");
        return null;
    }
}