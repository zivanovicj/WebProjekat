import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import {useState} from 'react';
import { useNavigate } from 'react-router-dom';
import NavDropdown from 'react-bootstrap/NavDropdown';

function Navigation(){
  const navigate = useNavigate();
  const [userType, setUserType] = useState(localStorage.getItem('userType'));
  const email = localStorage.getItem('email');

  const logoutHandler = (event) => {
    event.preventDefault();
    localStorage.removeItem('userType');
    localStorage.removeItem('token');
    localStorage.removeItem('email');
    setUserType(null);
    navigate('/');
    navigate(0);
  }

    return (
        <Navbar bg="dark" data-bs-theme="dark">
        <Container>
          <Navbar.Brand href="/">Web Shop</Navbar.Brand>
          <Nav className="ml-auto">
            {userType === null && <Nav.Link href="/register">Register</Nav.Link>}
            {userType === null && <Nav.Link href="/login">Log In</Nav.Link>}
            {userType === 'ADMIN' && 
              <NavDropdown title="Orders" id="basic-nav-dropdown">
                <NavDropdown.Item href="/customers">Customers</NavDropdown.Item>
                <NavDropdown.Item href="/sellers">Sellers</NavDropdown.Item>
                <NavDropdown.Item href="/allOrders">Orders</NavDropdown.Item>
              </NavDropdown>}
            {userType === 'CUSTOMER' && 
              <NavDropdown title="Orders" id="basic-nav-dropdown">
                <NavDropdown.Item href="/delivered">Delievered orders</NavDropdown.Item>
                <NavDropdown.Item href="/pending">Pending orders</NavDropdown.Item>
                <NavDropdown.Item href="/canceled">Canceled orders</NavDropdown.Item>
              </NavDropdown>}
            {userType === 'SELLER' && 
              <NavDropdown title="Orders" id="basic-nav-dropdown">
                <NavDropdown.Item href="/deliveredSeller">Delievered orders</NavDropdown.Item>
                <NavDropdown.Item href="/pendingSeller">Pending orders</NavDropdown.Item>
                <NavDropdown.Item href="/canceledSeller">Canceled orders</NavDropdown.Item>
              </NavDropdown>}
              {userType === 'SELLER' && 
              <NavDropdown title="Products" id="basic-nav-dropdown">
                <NavDropdown.Item href="/newProduct">New Product</NavDropdown.Item>
                <NavDropdown.Item href={'/myProducts/' + email}>My Products</NavDropdown.Item>
              </NavDropdown>}
            {userType !== null && <Nav.Link href={'/profile/' + email}>My Profile</Nav.Link>}
            {userType !== null && <Nav.Link onClick={logoutHandler}>Logout</Nav.Link>}
          </Nav>
        </Container>
      </Navbar>
      );
}

export default Navigation;