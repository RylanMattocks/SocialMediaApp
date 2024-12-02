import { useEffect, useState } from 'react';
import Post from '../components/Post.jsx';

function PostPopulator(){
    const[posts, setPosts] = useState([]);
    const[loaded, setLoaded] = useState(5);
    const[users, setUsers] = useState({});
    function loadMorePosts()
    {
        setLoaded(loaded + 10);
    }
    useEffect(() => {
        async function loadPosts()  {
          try {
            fetch("https://droptables.azurewebsites.net/api/Post")
          .then(response => response.json())
          .then(data => setPosts(data))
          } catch {
            setPosts([])
          }
      };
      loadPosts();}
      , []);
    useEffect(() => {
        posts.slice(0, loaded).forEach(post => {
          if (!users[post.userId]) {
            getUser(post.userId);
          }
        });
      }, [posts, loaded]);
    async function getUser(id)
      {
        if (users[id]) {
          return;
        }
        try {
          const response = await fetch(`https://droptables.azurewebsites.net/api/User/${id}`);
          const userData = await response.json();
          setUsers(prevUsers => ({ ...prevUsers, [id]: userData }));
        } catch (error) {
          console.error(`Error fetching user ${id}:`, error);
        }
      }
    return (
        <div>
            <div id="post-list">
                {posts?.slice(0, loaded).map(post => 
                {
                    const user = users[post.userId];

                    return(
                        <div key={post.postId}>
                        {user ?(
                                <Post postId = {post.postId} user = {user} content ={post.content}/>
                        ) : (
                            <p>Loading post...</p> 
                          )}
                        </div>
                    );
                }
            )}
            </div>
            <button id="load-more" type="button" onClick={loadMorePosts}>
                Load More
            </button>
                
        </div>
    )
}

export default PostPopulator;
