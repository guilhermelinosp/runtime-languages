function generateTree(nodeCount) {
  let tree = "";

  // Function to recursively generate tree
  function generateNode(depth) {
    let node = "";

    // Add current node with its random identifier
    let nodeId = Math.floor(Math.random() * Number.MAX_SAFE_INTEGER);
    node += "Node" + nodeId + "\n";

    // Add child nodes recursively if depth is not 0
    if (depth > 0) {
      for (let i = 0; i < 2; i++) {
        node += "  ".repeat(depth) + generateNode(depth - 1);
      }
    }

    return node;
  }

  // Generate the tree
  tree += generateNode(Math.floor(Math.log2(nodeCount)));

  return tree;
}

// Measure memory usage
const initialMemoryUsage = process.memoryUsage().heapUsed;

// Measure time
const start = process.hrtime.bigint();

const nodeCount = 1000000;
const treeText = generateTree(nodeCount);

const end = process.hrtime.bigint();

// Measure memory usage again
const finalMemoryUsage = process.memoryUsage().heapUsed;

const memoryUsage = finalMemoryUsage - initialMemoryUsage;
const executionTime = parseFloat(parseFloat(end - start) / 1e6); // Convert to milliseconds

console.log("Execution time: " + executionTime + " milliseconds");
console.log("Memory usage: " + memoryUsage + " bytes");
