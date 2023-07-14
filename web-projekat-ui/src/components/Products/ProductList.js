import Product from "./Product";
import NewOrder from "../Orders/NewOrder";
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

function ProductList(props){
    const products = props.products;

    return (
      <Container>
        <Row>
          <Col></Col>
          <Col><ul style={{display:"inline-block"}}>
                  {products.map(product => (
                    <Product
                      key = {product.productID}
                      product={product}
                    >
                    </Product>
                  ))}
                </ul>
          </Col>
          <Col>{localStorage.getItem('userType') === 'CUSTOMER' && <NewOrder/>}</Col>
        </Row>
      </Container>
      );
}
  
export default ProductList;