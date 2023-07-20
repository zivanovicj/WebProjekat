import Col from 'react-bootstrap/Col';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import Button from 'react-bootstrap/esm/Button';
import { ChangePassword } from '../../services/UserService';

function ChangePasswordForm(){
    const [validated, setValidated] = useState(false);
    const navigate = useNavigate();
    const [validationText, setValidationText] = useState("");

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
            email : localStorage.getItem('email'),
            oldPassword : document.getElementById("oldPassword").value,
            newPassword : document.getElementById("newPassword").value,
            newPasswordConfirm : document.getElementById("confirmPassword").value
        }

        await ChangePassword(data).then((response) => {
            navigate('/profile/' + data.email)
        }).catch((error) => {
            setValidationText("Something went wrong");
        })
    }

    return (
        <div>
          <Form onSubmit={handleSubmit} noValidate validated={validated} className="w-50 mt-5 mx-auto col-10 col-md-8 col-lg-6">
            <Form.Group as={Row} className="mb-3">
              <Form.Label column sm="2">
                Old Password
              </Form.Label>
              <Col sm="10">
                <Form.Control type="password" id="oldPassword" required/>
              </Col>
            </Form.Group>
      
            <Form.Group as={Row} className="mb-3">
              <Form.Label column sm="2">
                New Password
              </Form.Label>
              <Col sm="10">
                <Form.Control type="password" required id="newPassword"/>
              </Col>
            </Form.Group>

            <Form.Group as={Row} className="mb-3">
              <Form.Label column sm="2">
                Confirm Password
              </Form.Label>
              <Col sm="10">
                <Form.Control type="password" required id="confirmPassword"/>
              </Col>
            </Form.Group>
            <Form.Text id="validation" muted>{validationText}</Form.Text>
            <br/>
            <Button type="submit">Update password</Button>
          </Form>
          </div>
    );
}

export default ChangePasswordForm;