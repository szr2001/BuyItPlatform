import './CommentsDisplay.css'
import { Loading } from '../../Components'
import { useState,useEffect } from 'react';
function CommentsDisplay({ comments, onScrolledToBottomCallback }) {
    const [loading, setLoading] = useState(false);

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
                        <div>
                        </div>
                        :
                        <Loading/>
            }
        </div>
    );
}

export default CommentsDisplay;