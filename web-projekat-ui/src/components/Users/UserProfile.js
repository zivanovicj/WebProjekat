import Button from 'react-bootstrap/Button';
import Card from 'react-bootstrap/Card';
import { useParams } from 'react-router-dom';
import { useEffect, useState } from 'react';
import { GetUserDetails } from '../../services/UserService';

function UserProfile(){
    const {email} = useParams();
    const [user, setUser] = useState([]);
    const [message, setMessage] = useState('');
    const loggedIn = localStorage.getItem('userType');

    useEffect(()=>{
        const get = async() => {
            let info = {
                email: email,
                token: localStorage.getItem('token')
            }
            await GetUserDetails(info).then((response) => {
                setUser(response.data);
                setMessage('');
            }).catch((error) => {
                setUser([]);
                setMessage(error.response.data);
            });
        }
        get();
    }, [email])

    return (
        <div style={{margin: "auto",
            width: "20%",
            border: "3px",
            padding: "10px"}}>
        <Card style={{ width: '18rem' }}>
          <Card.Img variant="top" src={user.image} />
          <Card.Body>
            <Card.Title>{user.firstName} {user.lastName}</Card.Title>
            <Card.Subtitle className="mb-2 text-muted">Email: {user.email}</Card.Subtitle>
            <Card.Subtitle className="mb-2 text-muted">Username: {user.username}</Card.Subtitle>
            {Object.keys(user).includes('dateOfBirth') && <Card.Text>Date of birth: {user.dateOfBirth.split('T')[0]}</Card.Text>}
            <Card.Text>Address: {user.address}</Card.Text>
            {loggedIn === 'SELLER' && user.approved === 0 && <Card.Text>Status: <b>VERIFIED</b></Card.Text>}
            {loggedIn === 'SELLER' && user.approved === 1 && <Card.Text>Status: <b>REJECTED</b></Card.Text>}
            {loggedIn === 'SELLER' && user.approved === 2 && <Card.Text>Status: <b>IN PROCESS</b></Card.Text>}
            <Card.Subtitle>{message}</Card.Subtitle>
            <Button variant="primary" href={'/updateProfile/' + email}>Update profile</Button>
          </Card.Body>
        </Card>
        </div>
    );
}

export default UserProfile;