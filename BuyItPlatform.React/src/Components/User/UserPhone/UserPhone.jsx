import './UserPhone.css'
import { Loading } from '../../../Components';

function UserPhone({editable, phone}) {
    return phone ?
        (
            <div className="holder">
                {phone}
            </div>
        ) : (
            <div className="holder">
                <Loading />
            </div>
        );
}

export default UserPhone;