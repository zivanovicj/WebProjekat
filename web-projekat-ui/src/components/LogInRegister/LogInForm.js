import Col from 'react-bootstrap/Col';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import { useState } from 'react';
import Button from 'react-bootstrap/esm/Button';
import { LogIn } from '../../services/UserService';
import { useNavigate } from 'react-router-dom';
import { useGoogleLogin } from '@react-oauth/google';
import { GoogleLogIn, LogInGoogle } from '../../services/UserService';

function LogInForm (){
    const [validated, setValidated] = useState(false);
    const navigate = useNavigate();
    const [validationText, setValidationText] = useState("");

    const login = useGoogleLogin({
      onSuccess: async (codeResponse) => {
        await GoogleLogIn(codeResponse).then(async (result) => {
          await LogInGoogle(result.data).then((tokenInfo) => {
            let userType = '';
            localStorage.setItem('token', tokenInfo.data.token);
            if(tokenInfo.data.userType === "0")
              userType = 'ADMIN'
            else if(tokenInfo.data.userType === "1")
              userType = 'CUSTOMER'
            else
              userType = 'SELLER'  
            localStorage.setItem('userType', userType);
            navigate('/');
            navigate(0);
          }).catch((failMessage) => {
            setValidationText(failMessage.response.data);
          })
        }).catch((err) => {
          setValidationText(err.response.data);
        })
      },
      onError: (error) => setValidationText(error)
    });

    const handleSubmit = async (event) => {
        event.preventDefault();
        const form = event.currentTarget;
        if (form.checkValidity() === false) {
            event.preventDefault();
            event.stopPropagation();
            setValidationText("Please enter all fields");
        }
        else{
            setValidated(true);
            setValidationText("");

            const data = {
              Email : document.getElementById("email").value,
              Password : document.getElementById("password").value
            }
            await LogIn(data).then((response) => 
            {
              let userType = '';
              if(response.data.userType === "0")
                userType = 'ADMIN'
              else if(response.data.userType  === "1")
                userType = 'CUSTOMER'
              else
                userType = 'SELLER'  
              localStorage.setItem('userType', userType);
              localStorage.setItem('token', response.data.token);
              navigate('/');
              navigate(0);
            }
            ).catch((err) => {setValidationText(err.response.data)});
        }
    };

    return (
        <div>
        <Form onSubmit={handleSubmit} noValidate validated={validated} className="w-50 mt-5 mx-auto col-10 col-md-8 col-lg-6">
          <Form.Group as={Row} className="mb-3">
            <Form.Label column sm="2">
              Email
            </Form.Label>
            <Col sm="10">
              <Form.Control type="text" id="email" required defaultValue="email@example.com" />
            </Col>
          </Form.Group>
    
          <Form.Group as={Row} className="mb-3">
            <Form.Label column sm="2">
              Password
            </Form.Label>
            <Col sm="10">
              <Form.Control type="password" placeholder="Password" required id="password"/>
            </Col>
          </Form.Group>
          <Form.Text id="validation" muted>{validationText}</Form.Text>
          <br/>
          <Button type="submit">Log In</Button>
          <br/>
          <br/>
          <button onClick={() => login()}>Sign in with Google 🚀 </button>
        </Form>
        </div>
      );
}

export default LogInForm;