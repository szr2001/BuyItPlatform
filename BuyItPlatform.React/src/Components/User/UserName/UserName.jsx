import './UserName.css'
import { Loading } from '../../../Components';

function UserName({editable, name}) {
    return name ?
        (
            <div className="holder">
                {name}
            </div>
        ) : (
            <div className="holder">
                <Loading />
            </div>
        );
}

export default UserName;