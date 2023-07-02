import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';

function Navigation(){
    return (
        <Navbar bg="dark" data-bs-theme="dark">
        <Container>
          <Navbar.Brand href="/">Web Shop</Navbar.Brand>
          <Nav className="ml-auto">
            <Nav.Link href="#home">Register</Nav.Link>
            <Nav.Link href="#features">Log In</Nav.Link>
          </Nav>
        </Container>
      </Navbar>
      );
}

export default Navigation;