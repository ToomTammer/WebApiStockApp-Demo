# xUnit Attributes Cheat Sheet

xUnit provides several attributes to define and manage tests. Below is a detailed guide to these attributes and their usage.

## **1. [Fact]**

- **Purpose**: Used to define a test method that does not take any parameters.
- **Usage**: Apply this attribute to methods that should be executed as tests.
- **Example**:
  ```csharp
  public class MathTests
  {
      [Fact]
      public void AdditionTest()
      {
          int result = 2 + 2;
          Assert.Equal(4, result);
      }
  }

## **2. [Theory]**

- **Purpose**: Used to define a parameterized test method that takes parameters.
- **Usage**:  Apply this attribute to methods that need to run with multiple sets of input data.
- **Data Source Attributes**:  Use alongside [InlineData], [MemberData], or [ClassData] to provide the input data.
- **Example**:
  ```csharp
    public class MathTests
    {
        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(4, 5, 9)]
        public void AdditionTest(int a, int b, int expected)
        {
            int result = a + b;
            Assert.Equal(expected, result);
        }
    }

## **3. [InlineData]**

- **Purpose**:  Provides inline data for parameterized tests defined with [Theory].
- **Usage**:  Specify multiple sets of parameters directly within the attribute.
- **Example**:
  ```csharp
    public class MathTests
    {
        [Theory]
        [InlineData(3, 2, 5)]
        [InlineData(7, 8, 15)]
        public void AdditionTest(int a, int b, int expected)
        {
            int result = a + b;
            Assert.Equal(expected, result);
        }
    }

## **4. [MemberData]**

- **Purpose**: Provides data for parameterized tests from a property or method.
- **Usage**:  Define a property or method that returns IEnumerable<object[]> or IEnumerable<object>, and reference it in [Theory].
- **Example**:
  ```csharp
    public class MathTests
    {
        public static IEnumerable<object[]> TestData =>
            new List<object[]>
            {
                new object[] { 1, 2, 3 },
                new object[] { 4, 5, 9 }
            };

        [Theory]
        [MemberData(nameof(TestData))]
        public void AdditionTest(int a, int b, int expected)
        {
            int result = a + b;
            Assert.Equal(expected, result);
        }
    }

## **5. [ClassData]**

- **Purpose**: Provides data from a class that implements IEnumerable<object[]>.
- **Usage**:  Define a class that implements IEnumerable<object[]> to supply data to [Theory].
- **Example**:
    ```csharp
    public class MathTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 1, 2, 3 };
            yield return new object[] { 4, 5, 9 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class MathTests
    {
        [Theory]
        [ClassData(typeof(MathTestData))]
        public void AdditionTest(int a, int b, int expected)
        {
            int result = a + b;
            Assert.Equal(expected, result);
        }
    }

## **6. [Trait]**

- **Purpose**: Tags tests with key-value pairs for categorization.
- **Usage**: Apply to methods to assign tags that can be used for grouping and filtering tests.
- **Example**:
    ```csharp
        public class MathTests
        {
            [Fact]
            [Trait("Category", "Unit")]
            public void AdditionTest()
            {
                int result = 2 + 2;
                Assert.Equal(4, result);
            }
        }

## **7. [Collection]**

- **Purpose**: Groups tests that share a common setup or context.
- **Usage**: Define a collection and use it to manage shared context for tests.
- **Example**:
    ```csharp
    [CollectionDefinition("Database Collection")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
        // Collection definition
    }

    [Collection("Database Collection")]
    public class DatabaseTests
    {
        private readonly DatabaseFixture _fixture;

        public DatabaseTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }
    }

## **8. [ClassFixture]**

- **Purpose**: Provides shared setup and cleanup for tests within a class.
- **Usage**: Implement IClassFixture<T> in the test class to use shared context or setup.
- **Example**:
    ```csharp
    public class DatabaseFixture
    {
        public DatabaseFixture()
        {
            // Initialize database
        }
    }

    public class DatabaseTests : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _fixture;

        public DatabaseTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }
    }

## **9. [CollectionFixture]**

- **Purpose**: Provides shared context across multiple test classes.
- **Usage**: 'ICollectionFixture<T>' to share setup and cleanup across multiple test classes.
- **Example**:
    ```csharp
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
        // Collection definition
    }

    public class TestClass1 : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _fixture;

        public TestClass1(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }
    }

    public class TestClass2 : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _fixture;

        public TestClass2(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }
    }

## **10. [Skip]**

- **Purpose**: Skips a test method.
- **Usage**: Use when you want to skip certain tests, usually with a reason provided.
- **Example**:
    ```csharp
    public class SkippedTests
    {
        [Fact(Skip = "Skipping this test due to a known issue.")]
        public void SkippedTest()
        {
            // Test code
        }
    }
