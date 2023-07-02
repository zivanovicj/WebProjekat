import Card from 'react-bootstrap/Card';
import Button from 'react-bootstrap/Button';

function Product(props){
    return (
        <Card style={{ width: '18rem' }}>
          <Card.Body>
            <Card.Text style = {{fontWeight: 'bold'}}>{props.productName}</Card.Text>
            <Card.Subtitle className="mb-2 text-muted">{props.price} RSD</Card.Subtitle>
            <Card.Text>{props.description}</Card.Text>
            <Button variant="primary">Details</Button>
          </Card.Body>
        </Card>
      );
}

export default Product;