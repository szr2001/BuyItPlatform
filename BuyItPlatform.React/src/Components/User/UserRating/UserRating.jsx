import './UserRating.css'
import { Loading } from '../../../Components';
function UserRating({ editable, rating }) {
    return rating ?
        (
            <div className="holder">
                {rating}
            </div>
        ) : (
            <div className="holder">
                <Loading />
            </div>
        );
}

export default UserRating;