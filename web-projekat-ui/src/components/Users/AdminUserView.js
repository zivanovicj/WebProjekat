import Card from 'react-bootstrap/Card';
import Button from 'react-bootstrap/Button';
import {useState} from 'react';
import { VerifySeller, RejectSeller } from '../../services/AdminService';
import { useNavigate } from 'react-router-dom';

function AdminUserView(props) {
    const user = props.user;
    const [message, setMessage] = useState('');
    const navigate = useNavigate();

    const handleVerify = async () => {
        const data = {
            email: user.email,
            token: localStorage.getItem('token')
        }
        await VerifySeller(data).then((response) => {
            setMessage('')
            navigate(0);
        }).catch((error) => {
            console.log(error)
            setMessage(error.response.data);
        })
    }

    const handleReject = async () => {
        const data = {
            email: user.email,
            token: localStorage.getItem('token')
        }
        await RejectSeller(data).then((response) => {
            setMessage('')
            navigate(0);
        }).catch((error) => {
            console.log(error)
            setMessage(error.response.data);
        })
    }
    
    return (
        <Card style={{ width: '18rem' }}>
          <Card.Img variant="top" src={user.image} />
          <Card.Body>
            <Card.Title>{user.firstName} {user.lastName}</Card.Title>
            <Card.Subtitle className="mb-2 text-muted">Email: {user.email}</Card.Subtitle>
            <Card.Subtitle className="mb-2 text-muted">Username: {user.username}</Card.Subtitle>
            {Object.keys(user).includes('dateOfBirth') && <Card.Text>Date of birth: {user.dateOfBirth.split('T')[0]}</Card.Text>}
            <Card.Text>Address: {user.address}</Card.Text>
            {user.approved !== null && user.approved === 0 && <Card.Text>Status: VERIFIED</Card.Text>}
            {user.approved !== null && user.approved === 1 && <Card.Text>Status: REJECTED</Card.Text>}
            {user.approved !== null && user.approved === 2 && <Card.Text>Status: IN PROCESS</Card.Text>}
            {user.approved !== null && user.approved === 2 && <Button onClick={handleVerify} variant="primary">Verify</Button>}
            {user.approved !== null && user.approved === 2 && <Button onClick={handleReject} variant="primary">Reject</Button>}
            {user.approved !== null && user.approved === 2 && <Card.Subtitle className="mb-2 text-muted">{message}</Card.Subtitle>}
          </Card.Body>
        </Card>
    );
}

export default AdminUserView;