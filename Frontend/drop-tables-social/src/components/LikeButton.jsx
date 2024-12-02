import { useContext } from "react";
import { UserContext } from "../context/UserContext";

export default function LikeButton({postId, onChange}){
    const { currentUser, handleChange } = useContext(UserContext);

    const handleClick = async () => {
        try{
            const postLikeUrl = "https://droptables.azurewebsites.net/api/User/"+currentUser.userId+"/post/"+postId;
            const response = await fetch(postLikeUrl, {
                method: "POST"
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
        <button onClick={handleClick}>Like</button>
    )
}