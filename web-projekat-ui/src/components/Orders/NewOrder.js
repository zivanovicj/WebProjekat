import { useContext, useState } from 'react';
import { CartContext } from '../../store/CartContext';
import Card from 'react-bootstrap/Card';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import { CreateOrder } from '../../services/OrderService';

function NewOrder(){
    const { cartItems, removeFromCart, clearCart } = useContext(CartContext);
    const [validated, setValidated] = useState(false);
    const [validationText, setValidationText] = useState("");

    const handleSubmit = async (event) => {
        event.preventDefault();
        const form = event.currentTarget;
        if (form.checkValidity() === false){
            event.preventDefault();
            event.stopPropagation();
            setValidationText("Please enter all fields");
            return;
        }

        if(cartItems.length === 0){
            event.preventDefault();
            event.stopPropagation();
            setValidationText("No items selected");
            return;
        }

        setValidated(true);
        setValidationText("");

        const data = {
            comment: document.getElementById('comment').value,
            deliveryAddress: document.getElementById('address').value,
            orderedProducts: cartItems.map(item => {
                delete item.productName;
                return item;
            })
        }

        await CreateOrder(data).then((response) => {
            clearCart();
            setValidationText('Order placed successfully');
        }).catch((error) => {
            clearCart();
            setValidationText('Something went wrong')
        })
    }

    return (
        <div>
        <Form onSubmit={handleSubmit} noValidate validated={validated} style={{ width: '18rem' }}>
            <Form.Group as={Row} className="mb-3">
                <Form.Label column sm="2">Comment</Form.Label>
                <Form.Control type="text" id="comment" required/>
            </Form.Group>
            <Form.Group as={Row} className="mb-3">
                <Form.Label column sm="2">Delivery address</Form.Label>
                <Form.Control type="text" id="address" required/>
            </Form.Group>
            <Form.Text id="validation" muted>{validationText}</Form.Text>
            <br/>
            <br/>
            <Button type="submit">Place order</Button>
            <Button variant="primary" onClick={() => clearCart()}>Clear cart</Button>
        </Form>
        <br/>
        {cartItems.map(cartItem => (
            <Card style={{ width: '18rem' }} key={cartItem.productID}>
                <Card.Body>
                  <Card.Text style = {{fontWeight: 'bold'}}>Product name: {cartItem.productName}</Card.Text>
                  <Card.Subtitle className="mb-2 text-muted">Quantity: {cartItem.quantity}</Card.Subtitle>
                  <Button variant="primary" onClick={() => removeFromCart(cartItem.productID)}>Remove from cart</Button>
                </Card.Body>
            </Card>
        ))}
        </div>
      );
}

export default NewOrder;