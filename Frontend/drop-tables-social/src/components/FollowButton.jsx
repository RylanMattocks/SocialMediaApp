import { useContext } from "react";
import { UserContext } from "../context/UserContext";

export default function FollowButton({userId}){
    const { currentUser, handleChange } = useContext(UserContext);

    const handleClick = async () => {
        try{
            const postFollowUrl = "http://localhost:5001/api/User/"+currentUser.userId+"/user/"+userId;
            const response = await fetch(postFollowUrl, {
                method: "POST"
            });
            console.log(response);
            handleChange();
        }
        catch (error){
            console.log(error.message);
        }
    }
    return(
        <>
            {currentUser.userId == userId ? (<></>) : (<button onClick={handleClick}>Follow</button>)}
        </>
    )
}