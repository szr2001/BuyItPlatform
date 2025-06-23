import './CommentInput.css'
import { useState } from 'react';
import { useContext } from 'react';
import { AuthContext } from '../../Components/Auth/Auth'
import { Loading } from '../../Components'

function CommentInput({ onCommentedCallback, maxLength }) {
    const [authState, dispatch] = useContext(AuthContext);
    const [comment, setComment] = useState("");
    const [loading, setLoading] = useState(false);

    const sendComment = () =>
    {
        if (comment.length === 0 || comment.length > maxLength) return;
        setLoading(true);
        onCommentedCallback?.(comment).finally(() => {
            setLoading(false);
            setComment("");
        });
    }

    return (
        <div className="comment-input-holder">
            {
                authState.user.profileImgLink == " " ?
                    <svg className="comment-input-pic" viewBox="0 0 20 20" version="1.1" xmlns="http://www.w3.org/2000/svg" xmlns: xlink="http://www.w3.org/1999/xlink" fill="currentColor"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <title>profile_round [#1342]</title> <desc>Created with Sketch.</desc> <defs> </defs> <g id="Page-1" stroke="none" stroke-width="1" fill="currentColor" fill-rule="evenodd"> <g id="Dribbble-Light-Preview" transform="translate(-140.000000, -2159.000000)" fill="currentColor"> <g id="icons" transform="translate(56.000000, 160.000000)"> <path d="M100.562548,2016.99998 L87.4381713,2016.99998 C86.7317804,2016.99998 86.2101535,2016.30298 86.4765813,2015.66198 C87.7127655,2012.69798 90.6169306,2010.99998 93.9998492,2010.99998 C97.3837885,2010.99998 100.287954,2012.69798 101.524138,2015.66198 C101.790566,2016.30298 101.268939,2016.99998 100.562548,2016.99998 M89.9166645,2004.99998 C89.9166645,2002.79398 91.7489936,2000.99998 93.9998492,2000.99998 C96.2517256,2000.99998 98.0830339,2002.79398 98.0830339,2004.99998 C98.0830339,2007.20598 96.2517256,2008.99998 93.9998492,2008.99998 C91.7489936,2008.99998 89.9166645,2007.20598 89.9166645,2004.99998 M103.955674,2016.63598 C103.213556,2013.27698 100.892265,2010.79798 97.837022,2009.67298 C99.4560048,2008.39598 100.400241,2006.33098 100.053171,2004.06998 C99.6509769,2001.44698 97.4235996,1999.34798 94.7348224,1999.04198 C91.0232075,1998.61898 87.8750721,2001.44898 87.8750721,2004.99998 C87.8750721,2006.88998 88.7692896,2008.57398 90.1636971,2009.67298 C87.1074334,2010.79798 84.7871636,2013.27698 84.044024,2016.63598 C83.7745338,2017.85698 84.7789973,2018.99998 86.0539717,2018.99998 L101.945727,2018.99998 C103.221722,2018.99998 104.226185,2017.85698 103.955674,2016.63598" id="profile_round-[#1342]"> </path> </g> </g> </g> </g></svg>
                    :
                    <img className="comment-input-pic" src={authState.user.profileImgLink} />
            }
            <textarea disabled={loading} rows="1" cols="200" className="comment-input comment-input-text" autoComplete="off"
                maxLength={200} type="text" value={comment}
                onChange={(e) => {
                    setComment(e.target.value);
                    e.target.style.height = 'auto';
                    e.target.style.height = `${e.target.scrollHeight}px`;
                }}
                placeholder="comment..."></textarea>

            {
                loading ? 
                <Loading overrideClass={"comment-loading"} />
                :
                <button disabled={loading} onClick={sendComment} className="comment-input-btn">
                    <svg className="comment-input-btn-icon" viewBox="0 0 24 24" fill="currentColor" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <path d="M11.5003 12H5.41872M5.24634 12.7972L4.24158 15.7986C3.69128 17.4424 3.41613 18.2643 3.61359 18.7704C3.78506 19.21 4.15335 19.5432 4.6078 19.6701C5.13111 19.8161 5.92151 19.4604 7.50231 18.7491L17.6367 14.1886C19.1797 13.4942 19.9512 13.1471 20.1896 12.6648C20.3968 12.2458 20.3968 11.7541 20.1896 11.3351C19.9512 10.8529 19.1797 10.5057 17.6367 9.81135L7.48483 5.24303C5.90879 4.53382 5.12078 4.17921 4.59799 4.32468C4.14397 4.45101 3.77572 4.78336 3.60365 5.22209C3.40551 5.72728 3.67772 6.54741 4.22215 8.18767L5.24829 11.2793C5.34179 11.561 5.38855 11.7019 5.407 11.8459C5.42338 11.9738 5.42321 12.1032 5.40651 12.231C5.38768 12.375 5.34057 12.5157 5.24634 12.7972Z" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path> </g></svg>
                </button>
            }
        </div>
    );
}

export default CommentInput;