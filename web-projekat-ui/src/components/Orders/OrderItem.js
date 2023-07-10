import Card from 'react-bootstrap/Card';
import Button from 'react-bootstrap/Button';

function OrderItem(props){
    const orderItem = props.orderItem

    return (
        <Card style={{ width: '18rem' }}>
          <Card.Body>
            <Card.Text style = {{fontWeight: 'bold'}}>{orderItem.productName}</Card.Text>
            <Card.Text>Price: {orderItem.price}</Card.Text>
            <Card.Text>Quantity: {orderItem.quantity}</Card.Text>
            <Button href={'/details/' + orderItem.productID} variant="primary">Details</Button>
          </Card.Body>
        </Card>
    );
}

export default OrderItem;