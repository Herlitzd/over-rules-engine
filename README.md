# Definitions

**Entity** : A business object that is subject to business rules. The entity should be mutable, and it is assumed that it will have some facility for transactional writes.

**Policy** : A Set of values used to tune a rule. These are the knobs that can be turned to refine or alter how a rule behaves. These can be selected by either using data from within the entity being operated on, or through any other 
injectable means. Only a single policy can be selected, if more than one is selected, execution will stop.

**Rules** : An encapsulation of business logic that is able to determine how to interpret and apply a policy in order to alter the entity.
Rules produce an atomic mutation that can be applied, or alternatively, rolled back(?). A single rule can only execute 
once during a single execution. By default, only an ILogger can be injected. 

**Mutation** : A (reversible?) change that is applied to an entity. Each mutation is aware of what parts of the entity are being changed. If multiple mutations being applied operate on the same part of the entity, a collision will occur. Collisions can be resolved with an Resolver, or they can be configured to be ignored, if not handled, or ignored, execution will fail.

**Execution** : A single run through the rules engine. Before the execution, all policies and rules will be decided.


# Implementations

>Clear collision, EarlyBird and Holidays would need different rules for both to apply. Additionally, unclear the effect both policies could have if both executed.

```cs
class DiscountRule : IRule<IOrderEntity> {

}

interface IDiscountRulePolicy : IPolicy<IRule<IOrderEntity>>
{
  decimal discountPercent;
  decimal maxDiscountAmount;
}

class EarlyBirdPolicy : IDiscountRulePolicy 
{
  discountPercent = .10;
  maxDiscountAmount = 500;
}

class HolidayBirdPolicy : IDiscountRulePolicy 
{
  discountPercent = .15;
  maxDiscountAmount = 200;
}
```


> This seems to work
```cs
class DiscountRule : IRule<IOrderEntity> {

}

interface IDiscountRulePolicy : IPolicy<IRule<IOrderEntity>>
{
  decimal discountPercent;
  decimal maxDiscountAmount;
}

class MidMarketDiscount : IDiscountRulePolicy 
{
  discountPercent = .10;
  maxDiscountAmount = 500;
}

class SmallBusiness : IDiscountRulePolicy 
{
  discountPercent = .15;
  maxDiscountAmount = 200;
}
```

But how

```cs
class DiscountRule : IRule<IOrderEntity, IDiscountRulePolicy> {
  
  public DiscountRule(ILogger logger) {

  }

  internal Exec(IOrderEntity order, IDiscountPolicy policy) { 

    //Wrong
    order.applyDiscount(policy.discountPercent, policy.maxDiscountAmount);

    return new AddDiscountItemMutation(policy.discountPercent, policy.maxDiscountAmount);
  }
}

class AddDiscountItemMutation : IMutation<IOrderEntity> {
  ctor(...){}
  
  apply(decimal discountPercent, decimal maxDiscountAmount){
    order.applyDiscount(discountPercent, maxDiscountAmount);
  }

}
```


