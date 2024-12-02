import { useContext } from "react";
import { UserContext } from "../context/UserContext";

export default function DislikeButton({postId, onChange}){
    const { currentUser, handleChange } = useContext(UserContext);

    const handleClick = async () => {
        try{
            const postLikeUrl = "http://localhost:5001/api/User/"+currentUser.userId+"/post/"+postId;
            const response = await fetch(postLikeUrl, {
                method: "DELETE"
            });
            console.log(response);
            handleChange();
            onChange();
        }
        catch (error){
            console.log(error.message);
        }
    }
    return(
        <button onClick={handleClick}>Dislike</button>
    )
}