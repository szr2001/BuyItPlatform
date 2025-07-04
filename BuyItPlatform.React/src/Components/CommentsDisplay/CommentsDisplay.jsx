import './CommentsDisplay.css'
import { Loading } from '../../Components'
import { useNavigate } from 'react-router-dom';
import { useState, useEffect } from 'react';
import { useContext } from 'react';
import { toast } from 'react-toastify';
import { AuthContext } from '../../Components/Auth/Auth'
function CommentsDisplay({ comments, onScrolledToBottomCallback, onCommentDeleteCallback }) {
    const [loading, setLoading] = useState(false);
    const [authState, dispatch] = useContext(AuthContext);
    const navigate = useNavigate();

    const openProfile = (userId) => {
        navigate(`/Profile/${userId}`);
    }

    const getTimeAgo = (createdDate) => {
        if (createdDate === null) return "Now";

        const created = new Date(createdDate);
        const now = new Date();
        const diffMs = now - created;
        const diffMins = Math.floor(diffMs / (1000 * 60));
        const diffHours = Math.floor(diffMs / (1000 * 60 * 60));
        const diffDays = Math.floor(diffMs / (1000 * 60 * 60 * 24));

        if (diffDays >= 1) {
            return `${diffDays} day${diffDays > 1 ? 's' : ''} ago`;
        } else if (diffHours >= 1) {
            return `${diffHours} hour${diffHours > 1 ? 's' : ''} ago`;
        } else if (diffMins >= 1) {
            return `${diffMins} minute${diffMins > 1 ? 's' : ''} ago`;
        } else {
            return "Now";
        }
    }

    const deleteComment = async (e, commentId) => {
        e.target.disabled = true;
        onCommentDeleteCallback?.(commentId)
            .catch((error) => {
                e.target.disabled = false;
                const errorText = error.message || "An unexpected error occurred when deleting comment";
                toast.error(errorText, {
                    autoClose: 2000 + errorText.length * 50,
                });
            });
    }

    useEffect(() => {
        const handleScroll = () => {
            const scrollTop = window.scrollY;
            const windowHeight = window.innerHeight;
            const fullHeight = document.documentElement.scrollHeight;

            if (scrollTop + windowHeight >= fullHeight - 50) {
                if (!loading && comments) {
                    setLoading(true);
                    onScrolledToBottomCallback?.(comments.length).finally(() => {
                        setLoading(false);
                    });
                    console.log("END");
                }
            }
        };

        window.addEventListener("scroll", handleScroll);
        return () => window.removeEventListener("scroll", handleScroll);
    }, [loading, onScrolledToBottomCallback]);

    return (
        <div className="comments-holder" >
            {
                comments === null ?
                    null
                    :
                    comments.length > 0 ? 
                        comments.map((comment, i) => (
                            <div key={`comment-${i}`} onClick={() => { openProfile(comment.userId) } } className="comment fade-in">
                                {
                                    comment.userProfilePic == " " ?
                                    <svg className="comment-icon" viewBox="0 0 20 20" version="1.1" xmlns="http://www.w3.org/2000/svg" xmlns: xlink="http://www.w3.org/1999/xlink" fill="currentColor"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <title>profile_round [#1342]</title> <desc>Created with Sketch.</desc> <defs> </defs> <g id="Page-1" stroke="none" stroke-width="1" fill="currentColor" fill-rule="evenodd"> <g id="Dribbble-Light-Preview" transform="translate(-140.000000, -2159.000000)" fill="currentColor"> <g id="icons" transform="translate(56.000000, 160.000000)"> <path d="M100.562548,2016.99998 L87.4381713,2016.99998 C86.7317804,2016.99998 86.2101535,2016.30298 86.4765813,2015.66198 C87.7127655,2012.69798 90.6169306,2010.99998 93.9998492,2010.99998 C97.3837885,2010.99998 100.287954,2012.69798 101.524138,2015.66198 C101.790566,2016.30298 101.268939,2016.99998 100.562548,2016.99998 M89.9166645,2004.99998 C89.9166645,2002.79398 91.7489936,2000.99998 93.9998492,2000.99998 C96.2517256,2000.99998 98.0830339,2002.79398 98.0830339,2004.99998 C98.0830339,2007.20598 96.2517256,2008.99998 93.9998492,2008.99998 C91.7489936,2008.99998 89.9166645,2007.20598 89.9166645,2004.99998 M103.955674,2016.63598 C103.213556,2013.27698 100.892265,2010.79798 97.837022,2009.67298 C99.4560048,2008.39598 100.400241,2006.33098 100.053171,2004.06998 C99.6509769,2001.44698 97.4235996,1999.34798 94.7348224,1999.04198 C91.0232075,1998.61898 87.8750721,2001.44898 87.8750721,2004.99998 C87.8750721,2006.88998 88.7692896,2008.57398 90.1636971,2009.67298 C87.1074334,2010.79798 84.7871636,2013.27698 84.044024,2016.63598 C83.7745338,2017.85698 84.7789973,2018.99998 86.0539717,2018.99998 L101.945727,2018.99998 C103.221722,2018.99998 104.226185,2017.85698 103.955674,2016.63598" id="profile_round-[#1342]"> </path> </g> </g> </g> </g></svg>
                                    :
                                    <img className="comment-icon" src={comment.userProfilePic} />
                                }
                                <div className="comment-body">
                                        <div className="comment-body-name-holder">
                                            <label className="comment-body-name"> {comment.userName} </label>
                                            {
                                            authState.user.id === comment.userId ? 
                                                <button className="comment-body-delete" onClick={(e) => { e.stopPropagation(); deleteComment(e, comment.id); } }>
                                                    <svg viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <path d="M10 12V17" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path> <path d="M14 12V17" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path> <path d="M4 7H20" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path> <path d="M6 10V18C6 19.6569 7.34315 21 9 21H15C16.6569 21 18 19.6569 18 18V10" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path> <path d="M9 5C9 3.89543 9.89543 3 11 3H13C14.1046 3 15 3.89543 15 5V7H9V5Z" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path> </g></svg>
                                                </button>
                                                :
                                                null
                                            }
                                            <label className="comment-body-time"> {getTimeAgo(comment.createdDate)} </label>
                                        </div>
                                    <label className="comment-content"> { comment.content } </label>
                                </div>
                            </div>
                        ))
                        :
                        <Loading/>
            }
        </div>
    );
}

export default CommentsDisplay;