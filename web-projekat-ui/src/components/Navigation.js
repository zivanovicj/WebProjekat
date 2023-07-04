import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import {useState} from 'react';

function Navigation(){
  const [userType, setUserType] = useState(localStorage.getItem('userType'));

  const logoutHandler = (event) => {
    event.preventDefault();
    localStorage.removeItem('userType');
    localStorage.removeItem('token');
    setUserType(null);
  }

    return (
        <Navbar bg="dark" data-bs-theme="dark">
        <Container>
          <Navbar.Brand href="/">Web Shop</Navbar.Brand>
          <Nav className="ml-auto">
            {userType === null && <Nav.Link href="#home">Register</Nav.Link>}
            {userType === null && <Nav.Link href="/login">Log In</Nav.Link>}
            {userType !== null && <Nav.Link href="#profile">My Profile</Nav.Link>}
            {userType !== null && <Nav.Link onClick={logoutHandler}>Logout</Nav.Link>}
          </Nav>
        </Container>
      </Navbar>
      );
}

export default Navigation;