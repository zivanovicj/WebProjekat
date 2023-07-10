import Card from 'react-bootstrap/Card';
import Button from 'react-bootstrap/Button';

function Order(props){
    const order = props.order

    return (
        <Card style={{ width: '18rem' }}>
          <Card.Body>
            <Card.Text style = {{fontWeight: 'bold'}}>{order.customerID}</Card.Text>
            <Card.Subtitle className="mb-2 text-muted">{order.deliveryAddress} RSD</Card.Subtitle>
            <Card.Text>Time of order: {order.timeOfOrder.replace('T', ' ')}</Card.Text>
            <Card.Text>Delivery time: {order.deliveryTime.replace('T', ' ')}</Card.Text>
            <Card.Text>Price: {order.price} RSD</Card.Text>
            <Card.Text>Ordered items: {order.orderedProducts.length}</Card.Text>
            {order.orderStatus === 0 && <Card.Text>Order status: DELIEVERED</Card.Text>}
            {order.orderStatus === 1 && <Card.Text>Order status: IN PROGRESS</Card.Text>}
            {order.orderStatus === 2 && <Card.Text>Order status: CANCELED</Card.Text>}
            <Button href={'/orderDetails/' + order.orderID} variant="primary">Details</Button>
          </Card.Body>
        </Card>
    );
}

export default Order;