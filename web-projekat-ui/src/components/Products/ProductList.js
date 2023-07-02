import Product from "./Product";

function ProductList(props){
    const products = props.products;
    return (
        <div style={{textAlign:"center"}}>
        <ul style={{display:"inline-block"}}>
          {products.map(product => (
            <Product
              key = {product.productID}
              product={product}
            >
            </Product>
          ))}
        </ul>
        </div>
      );
}
  
export default ProductList;