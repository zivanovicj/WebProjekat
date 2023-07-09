import Col from 'react-bootstrap/Col';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import Button from 'react-bootstrap/esm/Button';
import { useState } from 'react';
import { UserRegistration } from '../../services/UserService';
import { useNavigate } from 'react-router-dom';

function RegisterUser(){
    const [validated, setValidated] = useState(false);
    const [validationText, setValidationText] = useState("");
    const [sellerChecked, setSellerChecked] = useState(false);
    const navigate = useNavigate();

    const handleSubmit = async (event) => {
        event.preventDefault();
        const form = event.currentTarget;
        if (form.checkValidity() === false) {
            event.preventDefault();
            event.stopPropagation();
            setValidationText("Please enter all fields");
            return;
        }
        setValidated(true);
        setValidationText("");
        
        const data = {
          email : document.getElementById("email").value,
          username: document.getElementById("username").value,
          firstName: document.getElementById("firstName").value,
          lastName: document.getElementById("lastName").value,
          address: document.getElementById("address").value,
          password : document.getElementById("password").value,
          passwordConfirm: document.getElementById("confpassword").value,
          dateOfBirth: document.querySelector('input[type="date"]').value,
          userType: 1
        }

        if(sellerChecked)
            data.userType = 2


        await UserRegistration(data).then((response) => {
            localStorage.setItem('token', response.data.token);
            localStorage.setItem('email', data.Email);
            if(sellerChecked)
                localStorage.setItem('userType', 'SELLER')
            else
                localStorage.setItem('userType', 'CUSTOMER')
            
            localStorage.setItem('token', response.data.token);
            localStorage.setItem('email', data.email);
            navigate('/profile/' + data.email)
            navigate(0);
        }).catch((error) => {
            if(Object.keys(error.response.data).includes('errors'))
                setValidationText("Please fill out all fields")
            else
                setValidationText(error.response.data)
        })
    }

    const handleSeller = () => {
        setSellerChecked(!sellerChecked);
    }

    return (
        <div>
            <Form onSubmit={handleSubmit} noValidate validated={validated} className="w-50 mt-5 mx-auto col-10 col-md-8 col-lg-6">
                <Form.Group as={Row} className="mb-3">
                    <Form.Label column sm="2">Email</Form.Label>
                    <Col sm="10">
                        <Form.Control type="text" id="email" required placeholder="email@email.com"/>
                    </Col>
                </Form.Group>
                <Form.Group as={Row} className="mb-3">
                    <Form.Label column sm="2">Username</Form.Label>
                    <Col sm="10">
                        <Form.Control type="text" placeholder="Username" required id="username"/>
                    </Col>
                </Form.Group>
                <Form.Group as={Row} className="mb-3">
                    <Form.Label column sm="2">First name</Form.Label>
                    <Col sm="10">
                        <Form.Control type="text" placeholder="First name" required id="firstName"/>
                    </Col>
                </Form.Group>
                <Form.Group as={Row} className="mb-3">
                    <Form.Label column sm="2">Last name</Form.Label>
                    <Col sm="10">
                        <Form.Control type="text" placeholder="Last name" required id="lastName"/>
                    </Col>
                </Form.Group>
                <Form.Group as={Row} className="mb-3">
                    <Form.Label column sm="2">Address</Form.Label>
                    <Col sm="10">
                        <Form.Control type="text" placeholder="Address" required id="address"/>
                    </Col>
                </Form.Group>
                <Form.Group as={Row} className="mb-3">
                    <Form.Label column sm="2">Date of birth</Form.Label>
                    <Col sm="10">
                        <input id="dateofBirth" type="date"/>
                    </Col>
                </Form.Group>
                <Form.Group as={Row} className="mb-3">
                    <Form.Label column sm="2">User type</Form.Label>
                    <Col sm="10">
                        <Form.Check type="switch" id="userType" label="Seller" checked={sellerChecked} onChange={handleSeller}/>
                    </Col>
                </Form.Group>
                <Form.Group as={Row} className="mb-3">
                    <Form.Label column sm="2">Password</Form.Label>
                    <Col sm="10">
                        <Form.Control type="password" placeholder="Password" required id="password"/>
                    </Col>
                </Form.Group>
                <Form.Group as={Row} className="mb-3">
                    <Form.Label column sm="2">Confirm password</Form.Label>
                    <Col sm="10">
                        <Form.Control type="password" placeholder="Confirm password" required id="confpassword"/>
                    </Col>
                </Form.Group>
                <Form.Text id="validation" muted>{validationText}</Form.Text>
                <br/>
                <br/>
                <Button type="submit">Register</Button>
            </Form>
        </div>
    )
}

export default RegisterUser;